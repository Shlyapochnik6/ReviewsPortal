namespace ReviewsPortal.Domain;

public class Like
{
    public Guid Id { get; set; }

    public bool Status { get; set; }

    public User User { get; set; }

    public Review Review { get; set; }
}