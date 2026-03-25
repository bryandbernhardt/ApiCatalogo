using Microsoft.AspNetCore.Mvc;
using ApiCatalogo.Filters;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;

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
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAll();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(c => c.Id == id);
            
            if (category is not null) return Ok(category);
            _logger.LogWarning("Category with id {Id} not found", id);
            return NotFound($"Category with id {id} not found");
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, Category category)
        {
            if (id != category.Id)
            {
                _logger.LogWarning("Id mismatch - id: {id}, categoryId: {categoryId}", id, category.Id);
                return BadRequest("Id mismatch");
            }

            var categoryUpdated = await _unitOfWork.CategoryRepository.Update(category);
            _unitOfWork.Commit();
            
            return Ok(categoryUpdated);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            await _unitOfWork.CategoryRepository.Create(category);
            _unitOfWork.Commit();
            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetById(c => c.Id == id);
            if (category != null)
            {
                await _unitOfWork.CategoryRepository.Delete(category);
                _unitOfWork.Commit();
            }
            return NoContent();
        }
    }
}
