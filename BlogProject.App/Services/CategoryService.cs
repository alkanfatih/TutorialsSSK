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
    public class CategoryService : ICategorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<int> CreateCategoryAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            return await _unitOfWork.CategoryRepo.AddAsync(category);
        }

        public async Task<int> DeleteCategoryAsync(string categoryId)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId);
            category.DeleteDate = DateTime.Now;
            category.Status = Core.DomainModels.Enums.EntityStatus.Deleted;
            return _unitOfWork.CategoryRepo.Delete(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepo.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetCategoryByIdAsync(string categoryId)
        {
            var category = await _unitOfWork.CategoryRepo.GetByIdAsync(categoryId);
            return _mapper.Map<CategoryDTO>(category);
        }

        public int UpdateCategory(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UpdateDate = DateTime.Now;
            category.Status = Core.DomainModels.Enums.EntityStatus.Updated;
            return _unitOfWork.CategoryRepo.Update(category);
        }
    }
}
