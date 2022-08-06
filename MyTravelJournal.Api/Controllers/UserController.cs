using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using MyTravelJournal.Api.Data;
using MyTravelJournal.Api.DTOs;
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
    public async Task<IActionResult> Register(UserRegisterRequestDto request)
    {
        if (_context.Users.Any(user => user.Username == request.Username))
            return BadRequest($"Username {request.Username} is already taken!");


        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var user = new User()
        {
            Username = request.Username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };


        _context.Users.Add(user);
        await _context.SaveChangesAsync();


        return Ok($"User {user.Username} has been registered");
    }


    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }
}