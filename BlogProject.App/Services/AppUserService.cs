using AutoMapper;
using BlogProject.App.DTOs;
using BlogProject.App.Services.IServices;
using BlogProject.App.Utilities.IUnitOfWorks;
using BlogProject.Core.DomainModels.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<int> CreateUserAsync(AppUserDTO user)
        {
            var appUser = _mapper.Map<AppUser>(user);
            return _unitOfWork.AppUserRepo.AddAsync(appUser);
        }

        public async Task<int> DeleteUserAsync(string userId)
        {
            var appUser = await _unitOfWork.AppUserRepo.GetByIdAsync(userId);
            return _unitOfWork.AppUserRepo.Delete(appUser);
        }

        public async Task<AppUserDTO> GetUserByIdAsync(string userId)
        {
            var appUser = await _unitOfWork.AppUserRepo.GetAllAsync();
            return _mapper.Map<AppUserDTO>(appUser);
        }

        public async Task<IEnumerable<AppUserDTO>> GetUsersAsync()
        {
            var appUser = await _unitOfWork.AppUserRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<AppUserDTO>>(appUser);
        }

        public int UpdateUserAsync(AppUserDTO user)
        {
            var appUser = _mapper.Map<AppUser>(user);
            return _unitOfWork.AppUserRepo.Update(appUser);
        }
    }
}
