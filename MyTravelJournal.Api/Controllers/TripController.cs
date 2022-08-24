using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyTravelJournal.Api.Data;
using MyTravelJournal.Api.DTOs;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
[Route("/api/trip")]
public class TripController : ControllerBase
{
    private readonly TravelJournalContext _context;

    public TripController(TravelJournalContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<List<Trip>>> GetAllTrips()
    {
        return Ok(await _context.Trips.ToListAsync());
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Trip>> GetTripById(int id)
    {
        var foundTrip = await _context.Trips.FirstOrDefaultAsync(trip => trip.TripId == id);

        if (foundTrip is null)
            return NotFound($"Trip with ID {id} was not found");

        return Ok(foundTrip);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrip([FromBody] Trip newData)
    {
        _context.Trips.Add(newData);
        await _context.SaveChangesAsync();
        return Ok("Trip successfully added");
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateTripById(int id, [FromBody] TripUpdateDto updatedData)
    {
        var foundTrip = await _context.Trips.FindAsync(id);

        if (foundTrip is null)
            return NotFound($"Trip with id {id} was not found.");

        foundTrip.Title = updatedData.Title;
        foundTrip.Description = updatedData.Description;
        foundTrip.Location = updatedData.Location;
        foundTrip.Start = updatedData.Start;
        foundTrip.End = updatedData.End;

        await _context.SaveChangesAsync();
        return Ok($"Trip data (id {id}) successfully updated.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTripById(int id)
    {
        var foundTrip = await _context.Trips.FindAsync(id);

        if (foundTrip is null)
            return NotFound($"Trip with id {id} was not found");

        _context.Trips.Remove(foundTrip);
        await _context.SaveChangesAsync();

        return Ok($"Trip with id {id} successfully deleted");
    }
}