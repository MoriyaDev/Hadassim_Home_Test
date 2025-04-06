using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using GroceryManager.Core.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {

        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Inventory>>> GetAll()
        {
            var inventory = await _inventoryService.GetAllAsync();
            return Ok(inventory);
        }

        [HttpPost("update-stock")]
        public async Task<IActionResult> UpdateStock([FromBody] SaleUpdateDto sale)
        {
            var missing = await _inventoryService.HandleSaleAndCheckStockAsync(sale.SoldItems);
            return Ok(new { message = "המלאי עודכן", missingProducts = missing });
        }


    }
}
