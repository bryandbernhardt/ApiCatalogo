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
        private readonly ICategoryRepository _repository;

        public CategoriesController(ILogger<CategoriesController> logger, ICategoryRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        // GET: api/Categories
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public async Task<ActionResult<IEnumerable<Category>>> GetAll()
        {
            var categories = await _repository.GetAll();
            return Ok(categories);
        }

        // GET: api/Categories/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Category>> GetById(int id)
        {
            var category = await _repository.GetById(c => c.Id == id);
            
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

            var categoryUpdated = await _repository.Update(category);
            return Ok(categoryUpdated);
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Category>> Create(Category category)
        {
            await _repository.Create(category);
            return CreatedAtAction("GetById", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repository.GetById(c => c.Id == id);
            if (category != null) await _repository.Delete(category);
            return NoContent();
        }
    }
}
