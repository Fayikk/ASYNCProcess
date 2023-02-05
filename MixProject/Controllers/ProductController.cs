using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MixProject.Business.Abstract;
using MixProject.Business.Concrete;
using MixProject.Business.Validation;
using MixProject.Entity;
using MixProject.Entity.Dto;
using static System.Net.Mime.MediaTypeNames;

namespace MixProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {

        IProductService _productService;
        IWebHostEnvironment _webHostEnvironment;
        public ProductController(IProductService productService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> Create(ProductDTO model)
        {

            ProductValidator validations = new ProductValidator();
            ValidationResult result = validations.Validate(model);


            if (result.IsValid)
            {
                var results = _productService.Create(model);
                return Ok(results);
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

        [HttpPost]
        [Route("upload")]
        [Authorize(Roles = "Admin")]

        public async Task<string> Upload([FromForm] ProductDTO product)
        {
            if (product.files.Length>0)
            {
                try
                {
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath+"\\Images\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Images\\");
                    }
                    using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath+"\\Images\\"+product.files.FileName))
                    {
                        product.files.CopyTo(fileStream);
                        fileStream.Flush();
                        return "\\Images\\" + product.files.FileName;
                    }
                }
                catch (Exception ex)
                {
                    return ex.ToString();
                }
            }
            else
            {
                return "upload Failed";
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = _productService.GetAll();
            if (result.IsCompleted)
            {
                return Ok(result);
            }
            return BadRequest(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute]int id)
        {
            var result = _productService.Delete(id);
            if (result.IsCompleted)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO model)
        {
            var result = _productService.Update(model);

            if (result.IsCompleted)
            {
                return Ok(result);
            }

            return BadRequest(result);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute ]int id)
        {
            var result = _productService.Get(id);
            if (result.IsCompleted)
            {
                return Ok(result);

            }
            return BadRequest(result);
        }

    }
}
