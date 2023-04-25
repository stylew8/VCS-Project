using DataAccess;
using DataAccessNew.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShopApi.Models.Users;
using ShopApi.Services.Users;
using ShopApi.Services;
using System.ComponentModel.DataAnnotations;
using DataAccessNew.Models;
using ShopApi.Models.Product;
using DataAccess.Repositories;
using ShopApi.Services.Products;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private IRepository<Product> _productRepository;
        private CreateProductService _createProductService;
        private LoggingService _loggingService;

        public ProductController(
            IRepository<Product> productRepository,
            CreateProductService createProductService,
            LoggingService loggingService)
        {
            _productRepository = productRepository;
            _createProductService = createProductService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var products = await _productRepository.ListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var product = await _productRepository.GetAsync(id);

            return Ok(product);
        }

        [HttpGet("name/{name}")]
        public async Task<IActionResult> GetToNameAsync(string name)
        {
            var product = await _productRepository.GetToNameAsync(name);

            return Ok(product);
        }

        [HttpGet("category/category/{category}")]
        public async Task<IActionResult> GetByCategoryAsync(string category)
        {
            var product = await _productRepository.GetByCategoryAsync(category);

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _productRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateProductModel userModel)
        {
            _loggingService.Write("ProductController started");

            try
            {
                var user = await _createProductService.CallAsync(userModel);

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
                _loggingService.Write("ProductController ended");
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(Product userModel, int id)
        {
            var user = await _productRepository.GetAsync(id);

            user.Name = userModel.Name;
            user.Description = userModel.Description;
            user.Quantity = userModel.Quantity;

            await _productRepository.UpdateAsync(user);

            return NoContent();
        }
    }
}
