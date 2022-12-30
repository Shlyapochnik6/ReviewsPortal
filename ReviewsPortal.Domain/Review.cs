namespace ReviewsPortal.Domain;

public class Review
{
    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
    
    public int Grade { get; set; }

    public string? ImageUrl { get; set; }

    public DateTime CreationDate { get; set; }

    public User User { get; set; }

    public Category Category { get; set; }

    public Art Art { get; set; } = new();

    public List<Tag> Tags { get; set; } = new();

    public List<Like> Likes { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();
}