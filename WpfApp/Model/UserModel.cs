﻿using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ShopApi.Models.User
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Premissions { get; set; } = "user";
    }
}
