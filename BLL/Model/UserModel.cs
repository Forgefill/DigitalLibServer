﻿
namespace BLL.Model
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } = "user";
    }
}
