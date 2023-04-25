namespace ShopApi.Models.Users
{
    public class UpdateUserModel : UserModel
    {
        public UpdateUserModel(
            int id,
            string username,
            string password,
            string name,
            string surname,
            string email,
            string premissions = "user") : base(
                                                  id,
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
