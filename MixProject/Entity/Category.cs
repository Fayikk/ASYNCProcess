using System.ComponentModel.DataAnnotations;

namespace MixProject.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string? CategoryName { get; set; }    
    }
}
