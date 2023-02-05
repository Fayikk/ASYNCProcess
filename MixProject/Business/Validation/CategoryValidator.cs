using FluentValidation;
using MixProject.Entity.Dto;

namespace MixProject.Business.Validation
{
    public class CategoryValidator:AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {

        }
    }
}
