using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MyTravelJournal.Api.Data;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly TravelJournalContext _context;

    public UserController(TravelJournalContext context)
    {
        _context = context;
    }


    [HttpPost("/register")]
    public async Task<IActionResult> Register(User request)
    {
        if (_context.Users.Any(user => user.Username == request.Username))
            return BadRequest($"Username {request.Username} is already taken!");


        return Ok();
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }
}