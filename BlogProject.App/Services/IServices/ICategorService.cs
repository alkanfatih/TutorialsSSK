using BlogProject.App.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.App.Services.IServices
{
    public interface ICategorService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(string categoryId);
        Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
        Task<int> CreateCategoryAsync(CategoryDTO categoryDto);
        int UpdateCategory(CategoryDTO categoryDto);
        Task<int> DeleteCategoryAsync(string categoryId);
    }
}
