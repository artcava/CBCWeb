using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CenturyBelongingCalculator.Web.Areas.Member.Pages.Calcs;

public class CreateCalc
{
    [BindProperty, Required, MinLength(8)]
    public required string CalcName { get; set; }
    [BindProperty, DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTimeOffset StartDate { get; set; }
}
