using DataAccess;
using DataAccessNew.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ShopApi.Models.Users;
using ShopApi.Services;
using ShopApi.Services.Users;
using System.ComponentModel.DataAnnotations;

namespace ShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IRepository<User> _userRepository;
        private CreateUserService _createUserService;
        private LoggingService _loggingService;

        public UserController(
            IRepository<User> userRepository,
            CreateUserService createUserService,
            LoggingService loggingService)
        {
            _userRepository = userRepository;
            _createUserService = createUserService;
            _loggingService = loggingService;
        }

        [HttpGet]
        public async Task<IActionResult> ListAsync()
        {
            var users = await _userRepository.ListAsync();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var student = await _userRepository.GetAsync(id);

            return Ok(student);
        }

        [HttpGet("username/{username}")]
        public async Task<IActionResult> GetByUsernameAsync(string username)
        {
            var student = await _userRepository.GetByUsernameAsync(username);

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateUserModel userModel)
        {
            _loggingService.Write("UsersController started");

            try
            {
                var user = await _createUserService.CallAsync(userModel);

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
                _loggingService.Write("UsersController ended");
            }

        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAsync(User userModel, int id)
        {
            var user = await _userRepository.GetAsync(id);

            user.Name = userModel.Name;
            user.Email = userModel.Email;
            user.Surname = userModel.Surname;
            user.Password = userModel.Password;
            user.Premissions = userModel.Premissions;

            await _userRepository.UpdateAsync(user);

            return NoContent();
        }

    }
}
