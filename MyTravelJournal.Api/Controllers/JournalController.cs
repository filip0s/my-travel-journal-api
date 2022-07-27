using Microsoft.AspNetCore.Mvc;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
[Route("/api/v1/journal")]
public class JournalController : Controller
{
    [HttpGet]
    public async Task<ActionResult<List<Journal>>> Get()
    {
        var journals = new List<Journal>
        {
            new Journal
            {
                Id = 0,
                Title = "Mount Fuji",
                Description = "Our trip to Japan ^^",
                Location = "Japan",
                Start = new DateTime(2022, 5, 24),
                End = new DateTime(2022, 5, 27),
            }
        };

        return Ok(journals);
    }
}