namespace ReviewsPortal.Domain;

public class Comment
{
    public Guid Id { get; set; }

    public string Text { get; set; }

    public DateTime CreationDate { get; set; }

    public User User { get; set; }

    public Review Review { get; set; }
}