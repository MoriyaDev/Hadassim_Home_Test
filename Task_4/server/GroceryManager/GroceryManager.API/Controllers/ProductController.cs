using GroceryManager.Core.Model;
using GroceryManager.Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet("by-supplier/{supplierId}")]
        public async Task<IActionResult> GetProductsBySupplier(int supplierId)
        {
            var products = await _productService.GetProductsBySupplierAsync(supplierId);
            return Ok(products);
        }


        // GET: api/<ProductController>
        //[HttpGet]
        //public IEnumerable<Product> Get()
        //{
        //    return null;
        //}

        // GET api/<ProductController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
