using BlogProject.App.IRepositories;
using BlogProject.Core.DomainModels.Models;
using BlogProject.Infrastructure.Contexts;
using BlogProject.Infrastructure.Repositories.BaseRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Infrastructure.Repositories
{
    public class AppUserRepo : BaseRepo<AppUser>, IAppUserRepo
    {
        private readonly AppDbContext _context;
        public AppUserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<AppUser> GetUserByEmailAsync(string userEmail)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
        }

        public async Task<AppUser> GetUserByNameAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}
