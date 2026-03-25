using ApiCatalogo.DTOs;
using Microsoft.AspNetCore.Mvc;
using ApiCatalogo.Models;
using ApiCatalogo.Repositories;
using AutoMapper;

namespace ApiCatalogo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        
        public ProductsController(IUnitOfWork unitOfWork,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        // GET: api/Products
        [HttpGet] public async Task<ActionResult<IEnumerable<ProductDTO>>> GetAll()
        {
            var products = await _unitOfWork.ProductRepository.GetAll();
            
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return Ok(productsDto);
        }

        // GET: api/Products/5
        [HttpGet("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }
            
            var productDto = _mapper.Map<ProductDTO>(product);
            return Ok(productDto);
        }

        [HttpGet("byCategory/{id:int:min(1)}")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetByCategory(int id)
        {
            var products = await _unitOfWork.ProductRepository.GetByCategoryId(id);
            var productsDto = _mapper.Map<IEnumerable<ProductDTO>>(products);
            
            return Ok(productsDto);
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> Update(int id, ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var product = _mapper.Map<Product>(productDto);
            var productUpdated = await _unitOfWork.ProductRepository.Update(product);
            _unitOfWork.Commit();

            if (productUpdated == null)
            {
                return NotFound();
            }

            var productUpdatedDto = _mapper.Map<ProductDTO>(productUpdated);
            return Ok(productUpdatedDto);
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO productDto)
        {
            var product = _mapper.Map<Product>(productDto);
            var newProduct = await _unitOfWork.ProductRepository.Create(product);
            _unitOfWork.Commit();
            
            var newProductDto = _mapper.Map<ProductDTO>(newProduct);
            return CreatedAtAction("GetById", new { id = newProductDto.Id }, newProductDto);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id:int:min(1)}")]
        public async Task<ActionResult<ProductDTO>> DeleteProduct(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetById(product => product.Id == id);
            if (product is null) return NotFound();
            
            var deletedProduct = await _unitOfWork.ProductRepository.Delete(product);
            _unitOfWork.Commit();
            
            var deletedProductDto = _mapper.Map<ProductDTO>(deletedProduct);
            return Ok(deletedProductDto);
        }
    }
}
