using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
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
    private readonly IMapper _mapper;

    public TripController(TravelJournalContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<Trip>>> GetAllTrips()
    {
        return Ok(await _context.Trips.ToListAsync());
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Trip>> GetTripById(int id)
    {
        var tripFromDatabase = await _context.Trips.FirstOrDefaultAsync(trip => trip.TripId == id);

        if (tripFromDatabase is null)
            return NotFound($"Trip with ID {id} was not found");

        return Ok(tripFromDatabase);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrip([FromBody] Trip newData)
    {
        _context.Trips.Add(newData);
        await _context.SaveChangesAsync();
        return Ok("Trip successfully added");
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateTripById(int id, JsonPatchDocument<Trip> request)
    {
        var tripFromDatabase = await _context.Trips.FindAsync(id);

        if (tripFromDatabase is null)
            return NotFound($"Trip with id {id} was not found.");

        request.ApplyTo(tripFromDatabase);

        await _context.SaveChangesAsync();
        return Ok($"Trip data (id {id}) successfully updated.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTripById(int id)
    {
        var tripFromDatabase = await _context.Trips.FindAsync(id);

        if (tripFromDatabase is null)
            return NotFound($"Trip with id {id} was not found");

        _context.Trips.Remove(tripFromDatabase);
        await _context.SaveChangesAsync();

        return Ok($"Trip with id {id} successfully deleted");
    }
}