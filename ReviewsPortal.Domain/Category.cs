namespace ReviewsPortal.Domain;

public class Category
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; }

    public List<Review> Reviews { get; set; } = new();
}