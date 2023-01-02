using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Persistence.Contexts;

public sealed class ReviewsPortalDbContext 
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IReviewsPortalDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<Art> Arts { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Like> Likes { get; set; }
    public DbSet<Rating> Ratings { get; set; }

    public ReviewsPortalDbContext(DbContextOptions<ReviewsPortalDbContext> options) 
        : base(options)
    {
        Database.Migrate();
    }
}