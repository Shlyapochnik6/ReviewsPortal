using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Application.Interfaces;

public interface IReviewsPortalDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Art> Arts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    int SaveChanges();
}