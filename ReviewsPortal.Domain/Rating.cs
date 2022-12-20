namespace ReviewsPortal.Domain;

public class Rating
{
    public Guid Id { get; set; }
    
    public int Value { get; set; }

    public User User { get; set; }

    public Art Art { get; set; } = new();
}