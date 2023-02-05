using FluentValidation;
using MixProject.Entity;
using MixProject.Entity.Dto;

namespace MixProject.Business.Validation
{
    public class ProductValidator : AbstractValidator<ProductDTO>
    {
        public ProductValidator()
        {
            
        }
    }
}
