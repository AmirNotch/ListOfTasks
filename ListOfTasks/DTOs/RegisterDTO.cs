using System.ComponentModel.DataAnnotations;

namespace ListOfTasks.DTOs;

public class RegisterDTO
{
    [Required]
    public string Name { get; set; }
        
    [Required]
    [EmailAddress]
    public string Email { get; set; }
        
    [Required]
    [RegularExpression("(?=.*\\d)(?=.*[a-z])(?=.*[A-Z]).{4,16}$", ErrorMessage = "Password must be complex")]
    public string Password { get; set; }
}