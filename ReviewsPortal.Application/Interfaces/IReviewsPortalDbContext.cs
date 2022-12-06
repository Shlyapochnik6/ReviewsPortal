using Microsoft.EntityFrameworkCore;
using ReviewsPortal.Domain;

namespace ReviewsPortal.Application.Interfaces;

public interface IReviewsPortalDbContext
{
    DbSet<User> Users { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}