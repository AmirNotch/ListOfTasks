using Domain;

namespace Application.Tasks;

public class TaskDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int Status { get; set; }
    public ListTask ListTask { get; set; }
    public ICollection<Comment> Comments { get; set; }
}