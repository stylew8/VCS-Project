using DataAccessNew.Models;
using DataAccessNew.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShopApi.Models.OrderLines;
using ShopApi.Services.OrderLines;
using ShopApi.Services;
using System.ComponentModel.DataAnnotations;
using ShopApi.Services.Orders;
using ShopApi.Models.Order;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private IRepository<Order> _orderRepository;
        private CreateOrderService _createOrderService;
        private LoggingService _loggingService;

        public OrderController(
            IRepository<Order> orderRepository,
            CreateOrderService createOrderService,
            LoggingService loggingService)
        {
            _orderRepository = orderRepository;
            _createOrderService = createOrderService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var order = await _orderRepository.ListAsync();

            return Ok(order);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var orderLine = await _orderRepository.GetAsync(id);

            return Ok(orderLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateOrderModel userModel)
        {
            _loggingService.Write("OrderController started");

            try
            {
                var user = await _createOrderService.CallAsync(userModel);

                return Ok(user);
            }
            catch (ValidationException ex)
            {
                _loggingService.Write(
                    $"{ex.Value} " +
                    $"{ex.ValidationResult.ErrorMessage} " +
                    $"{ex.ValidationResult.MemberNames.First()} " +
                    $"{ex.ToString()}");

                return BadRequest(ex.Message);
            }
            catch (SqlException ex)
            {
                _loggingService.Write(ex.ToString());
                return StatusCode(500, "Database problems");
            }
            catch (Exception ex)
            {
                _loggingService.Write(ex.ToString());
                return StatusCode(500, "Some problems");
            }
            finally
            {
                _loggingService.Write("OrderLineController ended");
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(Order userModel, int id)
        {
            var orderLine = await _orderRepository.GetAsync(id);

            orderLine.UserId = userModel.UserId;
            orderLine.IsPaid = userModel.IsPaid;

            await _orderRepository.UpdateAsync(orderLine);

            return NoContent();
        }
    }
}
