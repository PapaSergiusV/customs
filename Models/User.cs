using System.Collections.Generic;
using customs.ViewModels;

namespace customs.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<File> Files { get; set; }

        public User(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public User(SigninView signinView)
        {
            Email = signinView.Email;
            Password = signinView.Password;
        }
    }
}
