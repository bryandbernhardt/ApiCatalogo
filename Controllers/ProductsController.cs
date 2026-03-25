using Microsoft.AspNetCore.Mvc;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        
        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        // GET: api/Products
        [HttpGet] public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            return Ok(products);
        }

        // GET: api/Products/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpGet("byCategory/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(int id)
        {
            var products = await _unitOfWork.ProductRepository.GetByCategoryId(id);
            return Ok(products);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest("Id mismatch");
            }

            var productUpdated = await _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            if (productUpdated == null)
            {
                return NotFound();
            }

            return Ok(productUpdated);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            await _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();
            
            return CreatedAtAction("GetById", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(product => product.Id == id);
            if (product != null)
            {
                await _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.Commit();
            };
            return NoContent();
        }
    }
}
