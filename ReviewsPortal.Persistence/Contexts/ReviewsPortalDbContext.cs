using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Application.Interfaces;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Persistence.Contexts;

public class ReviewsPortalDbContext : IdentityDbContext<User>, IReviewsPortalDbContext
{
    public DbSet<User> Users { get; set; }

    public ReviewsPortalDbContext(DbContextOptions<ReviewsPortalDbContext> options)
        : base(options)
    {
        Database.Migrate();
    }
}