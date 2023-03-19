using System.ComponentModel.DataAnnotations;

namespace BLL.Model
{
    public class LoginModel
    { 
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }
    }
}
