using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Member.Pages.Calcs;

public class NewJoinDateModel : PageModelBase
{
    private readonly ILogger<NewJoinDateModel> _logger;
    private readonly ISender _sender;
    //private readonly Guid _event = new("{7DFC583B-A4ED-4FDB-A6B5-3EBA773BD35E}"); //Local
    private readonly Guid _event = new("{c28832da-0fd4-4831-8145-7d34dc22e4d6}"); //Remote

    public NewJoinDateModel(ILogger<NewJoinDateModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
        _logger.LogInformation("We initialize MyJoinDate model...");
    }

    [BindProperty]
    public CreateCalc Calc { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var eventResult = await _sender.Send(new GetEventByIdQuery { Id = _event });
            var command = new CreateCalcCommand
            {
                UserId = User.Identity.GetUserId(),
                CalcName = Calc.CalcName,
                EventId = _event,
                StartDate = Calc.StartDate,
                EndDate = eventResult.EventDate.AddDays((eventResult.EventDate - Calc.StartDate).Days)
            };

            var result = await _sender.Send(command);
            if (result == null) return BadRequest("Unable to create new Calc");
        }
        else
        {
            return BadRequest(ModelState);
        }
        return RedirectToPage("MyJoinDate");
    }
}
