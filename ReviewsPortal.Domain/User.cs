using Microsoft.AspNetCore.Identity;

namespace ReviewsPortal.Domain;

public class User : IdentityUser<Guid>
{
    public override Guid Id { get; set; }

    public int LikesCount { get; set; }

    public List<Like> Likes { get; set; } = new();

    public List<Rating> Ratings { get; set; } = new();

    public List<Comment> Comments { get; set; } = new();

    public List<Review> Reviews { get; set; } = new();
}