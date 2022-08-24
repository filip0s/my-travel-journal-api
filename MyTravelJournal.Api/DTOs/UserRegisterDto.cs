using System.ComponentModel.DataAnnotations;

namespace MyTravelJournal.Api.DTOs;

public class UserRegisterDto
{
    [Required] public string Username { get; set; } = string.Empty;

    [Required, MinLength(8, ErrorMessage = "Password should have at least 8 characters")]
    public string Password { get; set; } = string.Empty;

    [Required, Compare("Password", ErrorMessage = "Password confirmation does not match with password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}