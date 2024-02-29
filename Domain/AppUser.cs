using Microsoft.AspNetCore.Identity;

namespace Domain;

public class AppUser : IdentityUser
{
    // public string Id { get; set; }
    public string Name { get; set; }

    public ICollection<ListTask> ListTasks { get; set; } = new List<ListTask>();
}
