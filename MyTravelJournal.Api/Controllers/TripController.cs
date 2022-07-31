using Microsoft.AspNetCore.Mvc;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
[Route("/api/trip")]
public class TripController : ControllerBase
{
    // In-memory database
    private static List<Trip> _trips = new List<Trip>
    {
        new Trip
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
    public async Task<ActionResult<List<Trip>>> GetAllTrips()
    {
        return Ok(_trips);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Trip>> GetTripById(int id)
    {
        var foundTrip = _trips.FirstOrDefault(trip => trip.Id == id);

        if (foundTrip is null)
        {
            return NotFound($"Trip with ID {id} was not found");
        }

        return Ok(foundTrip);
    }

    [HttpPost]
    public async Task<ActionResult> AddTrip([FromBody] Trip newData)
    {
        _trips.Add(newData);
        return Ok("Trip successfully added");
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateTripById(int id, [FromBody] Trip updatedData)
    {
        var foundTrip = _trips.FirstOrDefault(trip => trip.Id == id);

        if (foundTrip is null)
        {
            return NotFound($"Trip with id {id} was not found.");
        }

        foundTrip.Title = updatedData.Title;
        foundTrip.Description = updatedData.Description;
        foundTrip.Location = updatedData.Location;
        foundTrip.Start = updatedData.Start;
        foundTrip.End = updatedData.End;

        return Ok($"Trip data (id {id}) sucessfully updated.");
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteTripById(int id)
    {
        var foundTrip = _trips.Find(trip => trip.Id == id);

        if (foundTrip is null)
        {
            return NotFound($"Trip with id {id} was not found");
        }

        _trips.Remove(foundTrip);

        return Ok($"Trip with id {id} sucessfully deleted");
    }
}