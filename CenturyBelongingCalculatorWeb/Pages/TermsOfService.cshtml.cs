using CenturyBelongingCalculator.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace CenturyBelongingCalculator.Web.Pages;

[AllowAnonymous]
public class TermsOfServiceModel : PageModelBase
{
    private readonly ILogger<TermsOfServiceModel> _logger;

    public TermsOfServiceModel(ILogger<TermsOfServiceModel> logger)
    {
        _logger = logger;
    }
    public void OnGet()
    {
    }
}
