using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Application.Features.Calcs.Queries;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Member.Pages.Calcs;

[Authorize(Roles = "Member")]
public class MyJoinDateModel : PageModelBase
{
    private readonly ILogger<CalcModel> _logger;
    private readonly ISender _sender;

    public MyJoinDateModel(ILogger<CalcModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
        _logger.LogInformation("We initialize MyJoinDate model...");
    }

    [BindProperty]
    public CalcModel Calc { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        _logger.LogInformation("We pass here...");
        var id = User.Identity.GetUserId();

        var query = new GetCalcByUserIdQuery { Id = id };
        var model = await _sender.Send(query);
        if (model != null)
        {
            Calc = new CalcModel
            {
                Id = model.Id,
                CalcName = model.CalcName,
                StartDate = model.StartDate,
                EndDate = model.EndDate

            };
            return Page();
        };
        return RedirectToPage("NewJoinDate");
    }
}
