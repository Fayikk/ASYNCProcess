using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MixProject.Business.Abstract;
using MixProject.Database;
using MixProject.Entity;
using MixProject.Entity.Dto;

namespace MixProject.Business.Concrete
{
    public class ProductService : IProductService
    {
        private IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public ProductService(ApplicationDbContext context,IMapper mapper, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<ProductDTO> Create(ProductDTO objDTO)
        {


            var obj = _mapper.Map<ProductDTO, Product>(objDTO);
            var addObj = _context.Products.Add(obj);
            obj.CreatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return _mapper.Map<Product, ProductDTO>(addObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                _context.Products.Remove(obj);
               await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<ProductDTO> Get(int id)
        {
            var obj = await _context.Products.Include(u => u.Category).FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return new ProductDTO();
        
        }

        public async Task<IEnumerable<ProductDTO>> GetAll()
        {
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDTO>>(_context.Products.Include(u => u.Category));
            
        }

    

        public async Task<ProductDTO> Update(ProductDTO objDTO)
        {
            var obj = await _context.Products.FirstOrDefaultAsync(x => objDTO.Id == x.Id);
            if (obj != null)
            {
                obj.Name = objDTO.Name;
                //obj.CategoryId = objDTO.CategoryId;
                _context.Products.Update(obj);
                return _mapper.Map<Product, ProductDTO>(obj);
            }
            return objDTO;
        }


     

    }
}
