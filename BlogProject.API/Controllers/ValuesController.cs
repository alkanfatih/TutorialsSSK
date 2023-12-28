using BlogProject.App.DTOs;
using BlogProject.App.Utilities.IUnitOfWorks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogProject.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ValuesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateCategory(CategoryDTO model) 
        { 
            var result = await _unitOfWork.CategorService.CreateCategoryAsync(model);
            if (result > 0)
                return Ok();
            else
                return BadRequest();
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetAllCategory()
        { 
            var result = await _unitOfWork.CategorService.GetAllCategoriesAsync();
            if (result == null)
                return BadRequest();
            else
                return Ok(result);
        }

    }
}
