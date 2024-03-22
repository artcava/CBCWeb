using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Admin.Pages.Events;

[Authorize(Roles = "Admin")]
public class DeleteEventModel : PageModelBase
{
    private readonly ILogger<DeleteEventModel> _logger;
    private readonly ISender _sender;

    public DeleteEventModel(ILogger<DeleteEventModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }
    [BindProperty]
    public DeleteEvent Event { get; set; }

    public async Task<IActionResult> OnGetAsync(Guid Id)
    {
        var query = new GetEventByIdQuery { Id = Id };
        var model = await _sender.Send(query);
        Event = new DeleteEvent
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
            EventDate = model.EventDate
        };
        return Page();
    }
    public async Task<IActionResult> OnPostAsync()
    {
        if(Event==null||Event.Id==Guid.Empty) return NotFound();

        var command = new DeleteEventCommand
        {
            Id = Event.Id,
        };
        var result = await _sender.Send(command);
        if (result <= 0) return BadRequest($"Unable to delete Event with Id:{Event.Id}");
        
        return RedirectToPage("Index");
    }
}
