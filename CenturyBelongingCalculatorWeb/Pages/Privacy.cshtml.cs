using Microsoft.AspNetCore.Authorization;

namespace CenturyBelongingCalculator.Web.Pages;

[AllowAnonymous]
public class PrivacyModel : PageModelBase
{
    private readonly ILogger<PrivacyModel> _logger;

    public PrivacyModel(ILogger<PrivacyModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
    }
}
