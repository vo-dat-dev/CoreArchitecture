using CoreArchitecture.Data;
using CoreArchitecture.Interface;
using CoreArchitecture.Models;
using CoreArchitecture.Reposititories;
using CoreArchitecture.Reposititories.StateMachines;
using Microsoft.AspNetCore.Mvc;

namespace CoreArchitecture.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _applicationContext;
        private readonly MasstransitSagaDbContext _sagaContext;

        public OrderController(IAuthentication authentication, IUserRepository userRepository,
            ApplicationDbContext applicationContext, MasstransitSagaDbContext sagaContext)
        {
            _applicationContext = applicationContext;
            _sagaContext = sagaContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest("Invalid data.");
            }
            _applicationContext.Orders.Add(order);
            await _applicationContext.SaveChangesAsync();

            // 2. Thêm dữ liệu vào MassTransitSagaDbContext (để cập nhật trạng thái saga)
            Console.WriteLine("Adding order to saga context..." + order.Id.ToString());
            var sagaOrder = new Order
            {
                Id = order.Id,
                CorrelationId = order.CorrelationId,
                CurrentState = "Pending", // Hoặc trạng thái mặc định cho saga
                OrderName = order.OrderName,
                OrderDate = order.OrderDate
            };
            _sagaContext.Orders.Update(sagaOrder);
            await _sagaContext.SaveChangesAsync();
            return CreatedAtAction(nameof(CreateOrder), new { id = order.Id }, order);
        }
    }
}