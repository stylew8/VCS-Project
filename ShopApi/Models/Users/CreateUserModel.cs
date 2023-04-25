using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopApi.Models.Users
{
    public class CreateUserModel : UserBaseModel
    {
        public CreateUserModel(
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
        }
    }
}
