using System.ComponentModel.DataAnnotations;

namespace Tourism.Dotnet.Admin.DTO;

public record UserRegisterDto([Required]string UserName, [Required]string Email, [Required]string Password);