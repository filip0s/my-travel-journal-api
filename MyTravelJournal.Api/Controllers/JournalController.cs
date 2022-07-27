using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
[Route("/api/journal")]
public class JournalController : Controller
{
    private static List<Journal> _journals = new List<Journal>
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

    [HttpGet]
    public async Task<ActionResult<List<Journal>>> GetAllJournals()
    {
        return Ok(_journals);
    }

    [HttpPost]
    public async Task<ActionResult<List<Journal>>> AddJournal([FromBody] Journal newData)
    {
        _journals.Add(newData);
        return Ok(_journals);
    }
}