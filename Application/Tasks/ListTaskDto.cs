using Domain;

namespace Application.Tasks;

public class ListTaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AppUser AppUser { get; set; }
    public ICollection<Task> Tasks { get; set; } 
}
