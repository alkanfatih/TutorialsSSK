﻿using _1_Pagination.Contexts;
using _1_Pagination.Models;
using _1_Pagination.Models.DTOs;
using _1_Pagination.TokenServices;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Dynamic.Core.Tokenizer;

namespace _1_Pagination.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class AccountController : ControllerBase
    {
        private readonly AppIdDbContext _idContext;
        private readonly IMapper _mapping;
        private readonly UserManager<AppUser> _userManager;
        private readonly MyTokenService _myTokenService;

        public AccountController(AppIdDbContext idContext, IMapper mapping, UserManager<AppUser> userManager, MyTokenService myTokenService)
        {
            _idContext = idContext;
            _mapping = mapping;
            _userManager = userManager;
            _myTokenService = myTokenService;
        }

        /// <summary>
        /// Add a new User to Register
        /// </summary>
        /// <param name="model">a user object</param>
        /// <remarks>
        /// Sample body content
        /// {"id":1, firstName="fatih", "lastName":"alkan"}
        /// </remarks>
        /// <returns code="200">Added</returns>
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

        /// <summary>
        /// Add a new User to Login
        /// </summary>
        /// <param name="model">a user object</param>
        /// <remarks>
        /// Sample body content
        /// {"id":1, firstName="fatih", "lastName":"alkan"}
        /// </remarks>
        /// <returns code="200">Added</returns>
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO model)
        {
            if (!await _myTokenService.ValidateUser(model))
                return Unauthorized();

            return Ok(new { Token = await _myTokenService.CreateToken() });
        }

    }
}
