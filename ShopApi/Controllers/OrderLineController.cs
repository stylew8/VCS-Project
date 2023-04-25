using DataAccess;
using DataAccessNew.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShopApi.Models.Users;
using ShopApi.Services.Users;
using ShopApi.Services;
using System.ComponentModel.DataAnnotations;
using DataAccessNew.Models;
using DataAccess.Repositories;
using ShopApi.Services.OrderLines;
using ShopApi.Models.OrderLines;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderLineController : Controller
    {
        private IRepository<OrderLine> _orderLineRepository;
        private CreateOrderLineService _createOrderLineService;
        private LoggingService _loggingService;

        public OrderLineController(
            IRepository<OrderLine> orderLineRepository,
            CreateOrderLineService createOrderLineService,
            LoggingService loggingService)
        {
            _orderLineRepository = orderLineRepository;
            _createOrderLineService = createOrderLineService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var orderLines = await _orderLineRepository.ListAsync();

            return Ok(orderLines);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var orderLine = await _orderLineRepository.GetAsync(id);

            return Ok(orderLine);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _orderLineRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateOrderLineModel userModel)
        {
            _loggingService.Write("OrderLineController started");

            try
            {
                var user = await _createOrderLineService.CallAsync(userModel);

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
        public async Task<IActionResult> UpdateAsync(OrderLine userModel, int id)
        {
            var orderLine = await _orderLineRepository.GetAsync(id);

            orderLine.OrderId = userModel.OrderId;
            orderLine.ProductId = userModel.ProductId;

            await _orderLineRepository.UpdateAsync(orderLine);

            return NoContent();
        }

        [HttpGet("userid/{userId}")]
        public async Task<IActionResult> GetByCategoryAsync(int userId)
        {
            var product = await _orderLineRepository.GetByUserIdAsync(userId);

            return Ok(product);
        }
    }
}
