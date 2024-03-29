using CenturyBelongingCalculator.Application;
using CenturyBelongingCalculator.Infrastructure;
using CenturyBelongingCalculator.Web.Areas.Identity.Data;
using CenturyBelongingCalculator.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

//SeriLog
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File($"log-{DateTimeOffset.Now:yyyy-MM}.txt",
    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

try
{
    Log.Information("Starting web application");

    var builder = WebApplication.CreateBuilder(args);
    var config = builder.Configuration;

    #region Add services to the container

    #region Add DbContext
    builder.Services.AddDbContext<CenturyBelongingCalculatorDbContext>(); //Configuration passed in DbContext.OnConfiguring ovverride for CenturyBelongingCalculatorDbContext

    builder.Services.AddDbContext<AuthenticationDbContext>(options => options.UseSqlServer(config.GetConnectionString("ApplicationDbContextConnection")));
    builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AuthenticationDbContext>();

    if (builder.Environment.IsDevelopment()) { builder.Services.AddDatabaseDeveloperPageExceptionFilter(); }
    #endregion


    builder.Services.AddApplicationServices();
    builder.Services.AddInfrastractureService(config);

    builder.Services.AddRazorPages();

    builder.Services.AddTransient<IEmailSender, EmailSender>();
    builder.Services.Configure<AuthMessageSenderOptions>(config);

    builder.Services.AddAuthentication() //Abilita ciascun servizio quando � disponibile.
        .AddFacebook(facebookOptions =>
        {
            IConfigurationSection facebookAuthNSection = config.GetSection("Authentication:Facebook");
            facebookOptions.AppId = facebookAuthNSection["ClientId"];
            facebookOptions.AppSecret = facebookAuthNSection["ClientSecret"];
            facebookOptions.AccessDeniedPath = "/AccessDeniedPathInfo";
        })
        .AddGoogle(googleOptions =>
        {
            IConfigurationSection googleAuthNSection = config.GetSection("Authentication:Google");
            googleOptions.ClientId = googleAuthNSection["ClientId"];
            googleOptions.ClientSecret = googleAuthNSection["ClientSecret"];
        })
        .AddMicrosoftAccount(microsoftOptions =>
        {
            IConfigurationSection microsoftAuthNSection = config.GetSection("Authentication:Microsoft");
            microsoftOptions.ClientId = microsoftAuthNSection["ClientId"];
            microsoftOptions.ClientSecret = microsoftAuthNSection["ClientSecret"];
        })
        .AddTwitter(twitterOptions =>
        {
            IConfigurationSection twitterAuthNSection = config.GetSection("Authentication:Twitter");
            twitterOptions.ClientId = twitterAuthNSection["ClientId"];
            twitterOptions.ClientSecret = twitterAuthNSection["ClientSecret"];
        });

    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdministratorRole",
                 policy => policy.RequireRole("Admin"));
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
    });

    #endregion

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseMigrationsEndPoint();
    }
    else
    {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapRazorPages();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}