using BlogProject.Core.DomainModels.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Configurations
{
    public class UserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasMany(u => u.Articles).WithOne(u => u.User).HasForeignKey(a => a.UserId);

            builder.HasMany(u => u.Comments).WithOne(u => u.User).HasForeignKey(c => c.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
