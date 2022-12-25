namespace ReviewsPortal.Domain;

public class Art
{
    public Guid Id { get; set; }
    
    public string ArtName { get; set; }

    public double AverageRating { get; set; }

    public Guid ReviewId { get; set; }

    public Review Review { get; set; }

    public List<Rating> Ratings { get; set; } = new();
}