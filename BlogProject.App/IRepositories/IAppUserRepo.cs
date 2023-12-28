using BlogProject.App.IRepositories.BaseRepos;
using BlogProject.Core.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.IRepositories
{
    public interface IAppUserRepo : IBaseRepo<AppUser>
    {
        Task<AppUser> GetUserByNameAsync(string userName);
        Task<AppUser> GetUserByEmailAsync(string userEmail);
    }
}
