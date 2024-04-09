using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoyaltyPointsExchangeSystem.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        public DbSet<PointsTransferHistory> pointsTransferHistories { get; set; }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}