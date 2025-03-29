using Microsoft.AspNetCore.Mvc;
using ShoppingCart.Models;
using ShoppingCart.Repositories;

namespace ShoppingCart.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return Ok(_productRepository.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<Product> Add(Product product)
        {
            _productRepository.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id,Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            _productRepository.Update(product);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _productRepository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            _productRepository.Delete(id);
            return NoContent();
        }
    }
}
