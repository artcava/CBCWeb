using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Admin.Pages.Events;

[Authorize(Roles = "Admin")]
public class CreateEventModel : PageModelBase
{
    private readonly ILogger<CreateEventModel> _logger;
    private readonly ISender _sender;

    public CreateEventModel(ILogger<CreateEventModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    [BindProperty]
    public CreateEvent Event { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var command = new CreateEventCommand
            {
                Description = Event.Description,
                Name = Event.Name,
                EventDate = Event.EventDate
            };
            var result = await _sender.Send(command);
            if (result == null) return BadRequest("Unable to create new Event");
        }
        else
        {
            return BadRequest(ModelState);
        }
        return RedirectToPage("Index");
    }
}
