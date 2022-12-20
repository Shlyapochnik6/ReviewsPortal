namespace ReviewsPortal.Domain;

public class Tag
{
    public Guid Id { get; set; }

    public string TagName { get; set; }

    public List<Review> Reviews { get; set; } = new();
}