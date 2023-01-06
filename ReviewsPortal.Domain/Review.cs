using System.ComponentModel.DataAnnotations.Schema;

namespace ReviewsPortal.Domain;

public class Review
{
    [NotMapped] 
    public string ObjectID { get; set; }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }
    
    public int Grade { get; set; }

    public DateTime CreationDate { get; set; }

    public User User { get; set; }

    public Category Category { get; set; }

    public Art Art { get; set; } = new();

    public List<Tag> Tags { get; set; } = new();

    public List<Like> Likes { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public List<Image> Images { get; set; } = new();
}