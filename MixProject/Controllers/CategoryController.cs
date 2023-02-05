using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixProject.Business.Abstract;
using MixProject.Business.Concrete;
using MixProject.Business.Validation;
using MixProject.Entity.Dto;

namespace MixProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService CategoryService)
        {
            _categoryService = CategoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           var result = _categoryService.GetAll();
            
            if(result == null)
            {
                return BadRequest("Category Not Found");
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        [Authorize(Roles="Admin")]

        public async Task<IActionResult> CreateCategory(CategoryDTO category)
        {
            CategoryValidator validations = new CategoryValidator();
            ValidationResult result = validations.Validate(category);


            if (result.IsValid)
            {
               await _categoryService.Create(category);
                return Ok();
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    return BadRequest(item.ErrorMessage);
                }
            }
            return null;

           
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            var result = _categoryService.Get(id);

            if (result.IsCompleted)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute]int id)
        {
            var result = _categoryService.Delete(id);
            if (result.IsCompleted)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(CategoryDTO model)
        {
            var result = await _categoryService.Update(model);
           
                return Ok(result);
         
        }

    }
}
