using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopApi.Models.Users
{
    public class UserBaseModel
    {
        public UserBaseModel(
            string username,
            string password,
            string name,
            string surname,
            string email,
            string premissions = "user")
        {
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Email = email;
            Premissions = premissions;
        }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Premissions { get; set; }
    }
}
