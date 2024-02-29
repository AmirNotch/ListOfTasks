namespace Domain;

public class ListTask
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public AppUser AppUser { get; set; }
    public ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
}
