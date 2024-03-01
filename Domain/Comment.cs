namespace Domain;

public class Comment
{
    public Guid Id { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Tasks Task { get; set; }
}
