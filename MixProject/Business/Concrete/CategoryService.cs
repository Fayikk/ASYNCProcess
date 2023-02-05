using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MixProject.Business.Abstract;
using MixProject.Database;
using MixProject.Entity;
using MixProject.Entity.Dto;

namespace MixProject.Business.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public CategoryService(ApplicationDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;   
        }

        public async Task<CategoryDTO> Create(CategoryDTO objDTO)
        {
            var obj = _mapper.Map<CategoryDTO, Category>(objDTO);
            var addedObj = _context.Add(obj);
           await _context.SaveChangesAsync();
            return _mapper.Map<Category, CategoryDTO>(addedObj.Entity);
        }

        public async Task<int> Delete(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if(obj != null)
            {
                _context.Remove(obj);
                return await _context.SaveChangesAsync();
            }
            return 0;
        }

        public async Task<CategoryDTO> Get(int id)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (obj != null)
            {
                return _mapper.Map<Category,CategoryDTO>(obj);
            }
            return new CategoryDTO();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAll()
        {
            return  _mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_context.Categories);
        }

        public async Task<CategoryDTO> Update(CategoryDTO objDTO)
        {
            var obj = await _context.Categories.FirstOrDefaultAsync(x => objDTO.Id == x.Id);
            if (obj != null)
            {
                obj.CategoryName = objDTO.CategoryName;
                _context.Categories.Update(obj);
                return _mapper.Map<Category, CategoryDTO>(obj);
            }
            return objDTO;
        }
    }
}
