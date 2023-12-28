using BlogProject.App.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services.IServices
{
    public interface IAppUserService
    {
        Task<AppUserDTO> GetUserByIdAsync(string userId);
        Task<IEnumerable<AppUserDTO>> GetUsersAsync();
        Task<int> CreateUserAsync(AppUserDTO user);
        int UpdateUserAsync(AppUserDTO user);
        Task<int> DeleteUserAsync(string userId);
    }
}
