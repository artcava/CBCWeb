using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Pages;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculatorWeb.Pages
{
    public class IndexModel : PageModelBase
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ISender _sender;

        public IndexModel(ILogger<IndexModel> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }

        public CalcModel Calc { get; set; }
        public async void OnGet()
        {
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
}
