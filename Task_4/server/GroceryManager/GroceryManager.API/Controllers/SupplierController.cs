using GroceryManager.Core.Dtos;
using GroceryManager.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        [HttpGet]
        public IActionResult GetSuppliers()
        {
            var suppliers = _supplierService.GetAllSuppliers();
            return Ok(suppliers);
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] SupplierRegisterDto dto)
        {
            var supplier = await _supplierService.RegisterSupplierAsync(dto);
            return Ok(supplier); 
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] SupplierLoginDto dto)
        {
            var supplier = await _supplierService.LoginAsync(dto);

            if (supplier == null)
                return Unauthorized("Phone number or password is incorrect.");

            return Ok(new
            {
                supplier.Id,
                supplier.CompanyName,
                supplier.AgentName
            });
        }

    }
}
