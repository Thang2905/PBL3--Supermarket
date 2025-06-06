using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Admin.Models;

public class LoginRequest
{
    [Required]
    public string Username { get; set; } = null!;
    [Required]
    public string Password { get; set; } = null!;
}

public class LoginResult
{
    [Required]
    public string Token { get; set; } = null!;
    [Required]
    public bool Authenticated { get; set; }
}

public class LoginResponse
{
    [Required]
    public int Code { get; set; }
    [Required]
    public string Message { get; set; } = string.Empty;
    [Required]
    public LoginResult? Result { get; set; }
}
