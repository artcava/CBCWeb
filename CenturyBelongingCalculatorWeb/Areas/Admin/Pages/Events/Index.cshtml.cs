using CenturyBelongingCalculator.Application.Features;
using CenturyBelongingCalculator.Web.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CenturyBelongingCalculator.Web.Areas.Admin.Pages.Events;

[Authorize(Roles ="Admin")]
public class IndexModel : PageModelBase
{
    private readonly ILogger<IndexModel> _logger;
    private readonly ISender _sender;

    public IndexModel(ILogger<IndexModel> logger, ISender sender)
    {
        _logger = logger;
        _sender = sender;
    }

    public IEnumerable<EventModel> Events { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        Events = await _sender.Send(new GetEventsQuery());
        return Page();
    }
}
