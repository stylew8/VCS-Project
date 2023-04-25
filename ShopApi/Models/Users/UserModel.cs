using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopApi.Models.Users
{
    public class UserModel : UserBaseModel
    {
        public UserModel(
            int id,
            string username,
            string password,
            string name,
            string surname,
            string email,
            string premissions = "user") : base(
                                                  username,
                                                  password,
                                                  name,
                                                  surname,
                                                  email,
                                                  premissions)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
