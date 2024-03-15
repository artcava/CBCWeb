using MediatR;

namespace CenturyBelongingCalculator.Web.Pages
{
    public class AuthorModel : PageModelBase
    {
        private readonly ILogger<AuthorModel> _logger;
        private readonly ISender _sender;

        public AuthorModel(ILogger<AuthorModel> logger, ISender sender)
        {
            _logger = logger;
            _sender = sender;
        }
        public void OnGet()
        {
        }
    }
}
