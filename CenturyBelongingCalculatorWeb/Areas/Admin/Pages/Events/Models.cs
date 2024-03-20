using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculator.Web.Areas.Admin.Pages.Events
{
    public class CreateEvent
    {
        [BindProperty, Required, MaxLength(64), MinLength(8)]
        public required string Name { get; set; }
        [BindProperty, Required, MinLength(64)]
        public required string Description { get; set; }
        [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EventDate { get; set; }
    }
    public class EditEvent
    {
        [BindProperty]
        public Guid Id { get; set; }
        [BindProperty, Required, MaxLength(64), MinLength(8)]
        public required string Name { get; set; }
        [BindProperty, Required, MinLength(64)]
        public required string Description { get; set; }
        [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EventDate { get; set; }
    }

    public class DeleteEvent
    {
        [BindProperty]
        public Guid Id { get; set; }
        [BindNever]
        public string Name { get; set; }
        [BindNever]
        public string Description { get; set; }
        [BindNever]
        public DateTimeOffset EventDate { get; set; }
    }
}
