using _1_Pagination.Contexts;
using _1_Pagination.Models;
using _1_Pagination.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace _1_Pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AppIdDbContext _idContext;
        private readonly IMapper _mapping;
        private readonly UserManager<AppUser> _userManager;

        public AccountController(AppIdDbContext idContext, IMapper mapping, UserManager<AppUser> userManager)
        {
            _idContext = idContext;
            _mapping = mapping;
            _userManager = userManager;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IdentityResult> RegisterUser(UserRegisterDTO model)
        { 
            var user = _mapping.Map<AppUser>(model);
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
                await _userManager.AddToRoleAsync(user, model.Role);

            return result;
        }
    }
}
