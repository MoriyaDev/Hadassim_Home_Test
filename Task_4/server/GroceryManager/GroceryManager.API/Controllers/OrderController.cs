using GroceryManager.Core.Dtos;
using GroceryManager.Core.Repositories;
using GroceryManager.Core.Service;
using GroceryManager.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GroceryManager.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }


        // GET: api/<OrderController>
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
                return NotFound();

            return Ok(order);
        }




        // GET api/<OrderController>/5
        [HttpGet("by-supplier/{supplierId}")]
        public async Task<IActionResult> GetOrdersBySupplier(int supplierId)
        {
            var orders = await _orderService.GetOrdersBySupplierIdAsync(supplierId);
            return Ok(orders);
        }


        // POST api/<OrderController>
        [HttpPost("{orderId}/approve")]
        public async Task<IActionResult> ApproveOrder(int orderId)
        {
            try
            {
                var updatedOrder = await _orderService.ApproveOrderAsync(orderId);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }

        [HttpPost("{id}/complete")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            try
            {
                var updatedOrder = await _orderService.MarkOrderAsCompletedAsync(id);
                return Ok(updatedOrder);
            }
            catch (Exception ex)
            {
                return NotFound(new { Message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
        {
            var order = await _orderService.CreateOrderAsync(dto);
            return Ok(order);
        }


        // DELETE api/<OrderController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
