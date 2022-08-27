using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyTravelJournal.Api.Data;
using MyTravelJournal.Api.DTOs;
using MyTravelJournal.Api.Models;

namespace MyTravelJournal.Api.Controllers;

[ApiController]
public class UserController : ControllerBase
{
    private readonly TravelJournalContext _context;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    // Constructor which is used for dependency injection
    // context - database context
    // mapper - configuration for AutoMapper
    // configuration - used for fetching configuration values from `appsettings.json`
    public UserController(TravelJournalContext context, IMapper mapper, IConfiguration configuration)
    {
        _context = context;
        _mapper = mapper;
        _configuration = configuration;
    }


    [HttpPost("/api/register")]
    public async Task<IActionResult> Register(UserRegisterDto request)
    {
        if (_context.Users.Any(user => user.Username == request.Username))
            return BadRequest($"Username {request.Username} is already taken!");

        CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);

        var createdUser = _mapper.Map<User>(request);
        // Manually setting password hash and salt for auto-mapped user
        createdUser.PasswordHash = passwordHash;
        createdUser.PasswordSalt = passwordSalt;

        _context.Users.Add(createdUser);
        await _context.SaveChangesAsync();

        return Ok($"User {createdUser.Username} has been registered");
    }


    [HttpPost("/api/login")]
    public async Task<ActionResult<string>> Login(UserLoginDto request)
    {
        // Searching for user in database by his username.
        var userFromDatabase = await _context.Users.FirstOrDefaultAsync(user => user.Username == request.Username);
        if (userFromDatabase is null)
            return NotFound("User not found.");

        // Checks if the provided password corresponds to hash stored in the database.
        if (!VerifyPasswordHash(request.Password, userFromDatabase.PasswordHash, userFromDatabase.PasswordSalt))
            return NotFound("Incorrect password");

        // Creates JWT token for particular user.
        var token = CreateToken(userFromDatabase);

        // Returns HTTP 200 with the JWT token.
        return Ok(token);
    }

    /// <summary>
    /// Creates password hash from supplied plain-text password and also generates salt
    /// </summary>
    /// <param name="password">Password string in plain-text form</param>
    /// <param name="passwordHash">Output parameter for hash computed from password</param>
    /// <param name="passwordSalt">Output parameter for generated salt</param>
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
    }

    private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512(passwordSalt);
        var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

        return computedHash.SequenceEqual(passwordHash);
    }

    private string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username)
        };

        var key = new SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        // Expiration date is set to tomorrow
        var expirationDate = DateTime.Now.AddDays(1);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: expirationDate,
            signingCredentials: cred
        );

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;
    }
}