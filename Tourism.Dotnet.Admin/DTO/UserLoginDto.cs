using System.ComponentModel.DataAnnotations;

namespace Tourism.Dotnet.Admin.DTO;

public record UserLoginDto([Required] string Email, [Required] string Password);