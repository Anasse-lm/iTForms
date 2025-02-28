using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace iTFORMS.Models;

public class User : IdentityUser<Guid>
{
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; } = "";

    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; } = "";

    public List<Template> Templates { get; set; } = [];
}
