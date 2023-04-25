using DataAccess;
using DataAccessNew.Repositories;
using ShopApi.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopApi.Services.Users
{
    public class CreateUserService
    {
        private readonly IRepository<User> _userRepository;

        public CreateUserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> CallAsync(CreateUserModel model)
        {
            Validate(model);

            var studentEntity = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email,
                Password = model.Password,
                Username = model.Username,
                Premissions = model.Premissions
            };

            await _userRepository.CreateAsync(studentEntity);

            return new UserModel(
                studentEntity.Id,
                studentEntity.Username,
                studentEntity.Password,
                studentEntity.Name,
                studentEntity.Surname,
                studentEntity.Email,
                studentEntity.Premissions
            );
        }

        private void Validate(CreateUserModel model)
        {
            // email
            if (!IsEmailValid(model.Email))
            {
                var validationResult = new ValidationResult(
                    "Blogas el. paštas",
                    new[] { "Email" }
                );

                throw new ValidationException(validationResult, null, model.Email);
            }

            if (!IsUsernameValid(model.Username))
            {
                var validationResult = new ValidationResult(
                    $"Blogas vartotojo vardas:" +
                    $"Vartotojo vardai turi prasidėti raide [a-z].\r\n" +
                    $"Vartotojo vardai turi baigtis [a-z,0-9].\r\n" +
                    $"Vartotojo vardai gali būti ilgio nuo 3 iki 32 simbolių.\r\n" +
                    $"Vartotojo vardai gali turėti bet kurį iš simbolių [a-z,0-9._-].\r\n" +
                    $"Skaičiai neturi būti arti vienas kito daugiau nei 4 kartus. " +
                    $"Tai reiškia, kad vartotojo vardas \"p1234\" atitinka reikalavimus, o \"p12345\" ne.\r\n" +
                    $"Kiekvienas vartotojo vardas gali turėti tik vieną iš simbolių [._-]." +
                    $" Tai reiškia, kad vartotojo vardas gali turėti tik tašką, brūkšnį arba pabraukimą.\r\n" +
                    $"Kiekvienas taškas, brūkšnys ir pabraukimas turi būti sekamas alfanumeriniu simboliu." +
                    $" Tai reiškia, kad taškas negali būti sekamas kitu tašku. Taip pat šie simboliai neturi būti arti vienas kito.",
                    new[] { "Email" }
                );

                throw new ValidationException(validationResult, null, model.Email);
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                var validationResult = new ValidationResult(
                    "Vardas privalomas",
                    new[] { "Name" }
                );

                throw new ValidationException(validationResult, null, null);
            }

            if (string.IsNullOrEmpty(model.Surname))
            {
                var validationResult = new ValidationResult(
                    "Pavardė privalomaa",
                    new[] { "Surnae" }
                );

                throw new ValidationException(validationResult, null, null);
            }
        }

        public bool IsUsernameValid(string username)
        {
            string pattern = @"^(?=.{3,32}$)(?!.*[._-]{2})(?!.*[0-9]{5,})[a-z](?:[\w]*|[a-z\d\.]*|[a-z\d-]*)[a-z0-9]$";

            return Regex.IsMatch(username, pattern);
        }

        private bool IsEmailValid(string email)
        {
            string pattern = @"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$";

            return Regex.IsMatch(email, pattern);

        }

    }
}
