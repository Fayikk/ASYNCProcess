using System.ComponentModel.DataAnnotations.Schema;

namespace MixProject.Entity.Dto
{
    public class ProductDTO
    {

        public int Id { get; set; }
        public string? Name { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IFormFile files { get; set; }
    }
}
