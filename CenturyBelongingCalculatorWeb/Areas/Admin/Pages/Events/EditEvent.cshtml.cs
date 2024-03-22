using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Admin.Pages.Events;

[Authorize(Roles = "Admin")]
public class EditEventModel : PageModelBase
{
    private readonly ILogger<CreateEventModel> _logger;
    private readonly ISender _sender;

    public EditEventModel(ILogger<CreateEventModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [BindProperty]
    public EditEvent Event { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid Id)
    {
        var query = new GetEventByIdQuery { Id = Id };
        var model = await _sender.Send(query);
        Event = new EditEvent
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            BeforeEventLabel = model.BeforeEventLabel ?? string.Empty,
            AfterEventLabel = model.AfterEventLabel ?? string.Empty,
            EventDate = model.EventDate
        };
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if (ModelState.IsValid)
        {
            var command = new UpdateEventCommand
            {
                Id = Event.Id,
                Name = Event.Name,
                Description = Event.Description,
                BeforeEventLabel = Event.BeforeEventLabel,
                AfterEventLabel = Event.AfterEventLabel,
                EventDate = Event.EventDate
            };
            var result = await _sender.Send(command);
            if (result <= 0) return BadRequest("Unable to create new Event");
        }
        else
        {
            return BadRequest(ModelState);
        }
        return RedirectToPage("Index");
    }
}
