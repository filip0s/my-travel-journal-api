using System.ComponentModel.DataAnnotations;

namespace MyTravelJournal.Api.DTOs;

public class UserLoginDto
{
    [Required] public string Username { get; set; } = string.Empty;

    [Required, MinLength(8, ErrorMessage = "Password should have at least 8 characters")]
    public string Password { get; set; } = string.Empty;
}