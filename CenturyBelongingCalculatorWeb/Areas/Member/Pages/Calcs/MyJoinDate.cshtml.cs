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
    }

    [BindProperty]
    public CalcModel Calc { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        var id = User.Identity.GetUserId();

        var query = new GetCalcByUserIdQuery { Id = id };
        Calc = await _sender.Send(query);
        if (Calc != null)
        {
            return Page();
        };
        return RedirectToPage("NewJoinDate");
    }

    public async Task<ActionResult> OnGetChartData()
    {
        Calc = await _sender.Send(new GetDefaultCalcQuery());

        var chart = new Chart
        {
            cols = new object[]
            {
                new { id = "belonging", type = "string", label = "Belonging" },
                new { id = "days", type = "number", label = "Days" }
            },
            rows = new object[]
            {
                new { c = new object[] { new { v = "Days after (XXI century)" }, new { v = Calc.DaysAfterEvent } } },
                new { c = new object[] { new { v = "Days before (XX century)" }, new { v = Calc.DaysBeforeEvent } } },
            },
            title = "Belonging calculator",
            calcName = Calc.CalcName,
            name = Calc.EventName,
            description = Calc.EventDescription,
            joinDate = Calc.JoinDate
        };

        return new JsonResult(chart);
    }
    public class Chart
    {
        public object[] cols { get; set; }
        public object[] rows { get; set; }
        public string title { get; set; }
        public string calcName { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTimeOffset joinDate { get; set; }
    }

}
