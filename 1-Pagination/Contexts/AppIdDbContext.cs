using _1_Pagination.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _1_Pagination.Contexts
{
    public class AppIdDbContext : IdentityDbContext<AppUser>
    {
        public AppIdDbContext(DbContextOptions<AppIdDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(
                    new IdentityRole 
                    { 
                        Name = "User",
                        NormalizedName = "USER"
                    },
                    new IdentityRole
                    { 
                        Name = "Editor",
                        NormalizedName = "EDITOR"
                    },
                    new IdentityRole
                    { 
                        Name = "Admin",
                        NormalizedName = "ADMIN"
                    }
                );
            base.OnModelCreating(builder);
        }
    }
}
