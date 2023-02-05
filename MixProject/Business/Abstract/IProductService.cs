using MixProject.Entity;
using MixProject.Entity.Dto;

namespace MixProject.Business.Abstract
{
    public interface IProductService
    {
        public Task<ProductDTO> Create(ProductDTO objDTO);
        public Task<ProductDTO> Update(ProductDTO objDTO);
        public Task<int> Delete(int id);
        public Task<ProductDTO> Get(int id);
        public Task<IEnumerable<ProductDTO>> GetAll();

    }
}
