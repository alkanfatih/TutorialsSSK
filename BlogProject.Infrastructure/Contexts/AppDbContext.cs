using BlogProject.Core.DomainModels.Models;
using BlogProject.Infrastructure.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Contexts
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Article> Articles { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=GNRM-EGT-0TLRXA\\ADMINSERVER;Database=BlogProjectSGK;User Id=sa;Password=Password1;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new CategoryConfig());
            builder.ApplyConfiguration(new ArticleConfig());
            builder.ApplyConfiguration(new CommentConfig());
            builder.ApplyConfiguration(new UserConfig());
            base.OnModelCreating(builder);
        }
    }
}
