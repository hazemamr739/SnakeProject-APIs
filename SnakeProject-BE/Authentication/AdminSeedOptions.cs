using System.ComponentModel.DataAnnotations;

namespace SnakeProject.API.Authentication;

public class AdminSeedOptions
{
    public static string SectionName => "AdminSeed";

    [Required]
    [EmailAddress]
    public string Email { get; init; } = string.Empty;

    [Required]
    [MinLength(8)]
    public string Password { get; init; } = string.Empty;

    public string FirstName { get; init; } = "System";
    public string LastName { get; init; } = "Admin";
}
