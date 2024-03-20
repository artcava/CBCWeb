using CenturyBelongingCalculator.Web.Services;
using Microsoft.AspNetCore.Authorization;

namespace CenturyBelongingCalculator.Web.Pages;

[AllowAnonymous]
public class AccessDeniedPathInfoModel : PageModelBase
{
    public void OnGet()
    {
    }
}
