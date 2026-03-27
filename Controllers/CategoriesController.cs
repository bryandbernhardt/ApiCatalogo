using ApiCatalogo.DTOs;
using ApiCatalogo.DTOs.Mappings;
using Microsoft.AspNetCore.Mvc;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Pagination;
using ApiCatalogo.Repositories;
using Newtonsoft.Json;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CategoriesController(ILogger<CategoriesController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Categories
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetAll([FromQuery] CategoriesParameters categoriesParameters)
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll(categoriesParameters);
            
            var metadata = new
            {
                categories.TotalCount,
                categories.PageSize,
                categories.CurrentPage,
                categories.TotalPages,
                categories.HasNext,
                categories.HasPrevious,
            };
            Response.Headers.Append("Pagination", JsonConvert.SerializeObject(metadata));

            var categoriesDto = categories.ToCategoryDtoList();

            return Ok(categoriesDto);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(c => c.Id == id);
            
            if (category is not null)
            {
                var categoryDto = category.ToCategoryDto();
                return Ok(categoryDto);
            }
            _logger.LogWarning("Category with id {Id} not found", id);
            return NotFound($"Category with id {id} not found");
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> Update(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                _logger.LogWarning("Id mismatch - id: {id}, categoryId: {categoryId}", id, categoryDto.Id);
                return BadRequest("Id mismatch");
            }

            var category = categoryDto.ToCategory();

            var updatedCategory = await _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            
            var updatedCategoryDto = updatedCategory.ToCategoryDto();
            
            return Ok(updatedCategoryDto);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> Create(CategoryDTO categoryDto)
        {
            var category = categoryDto.ToCategory();
            
            var newCategory = await _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();

            var newCategoryDto = newCategory.ToCategoryDto();
            
            return CreatedAtAction("GetById", new { id = newCategoryDto.Id }, newCategoryDto);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(c => c.Id == id);
            
            if (category == null) return NotFound($"Category with id {id} not found");
            
            await _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Commit();

            var deletedCategoryDto = category.ToCategoryDto();
            return  Ok(deletedCategoryDto);
        }
    }
}
