using System.Linq;
using Microsoft.AspNetCore.Hosting;
using customs.Models;
using customs.ViewModels;

namespace customs
{
    public class UserService : IUserService
    {
        private Context _db;
        private IWebHostEnvironment _appEnvironment;
        public UserService(IWebHostEnvironment appEnvironment, Context context)
        {
            _db = context;
            _appEnvironment = appEnvironment;
        }

        public User SignIn(SigninView signinView)
        {
            User user = _db.Users
                .FirstOrDefault(u => u.Email == signinView.Email && u.Password == signinView.Password);
            return user;
        }

        public User SignUp(SignupView signupView)
        {
            User user = SignIn(signupView);
            if (user != null)
                return user;
            user = new User(signupView);
            _db.Users.Add(user);
            _db.SaveChanges();
            return user;
        }

        public bool Exists(SigninView signinView)
        {
            return _db.Users.FirstOrDefault(u => u.Email == signinView.Email) != null;
        }

        public async void Destroy(string email)
        {
            User user = _db.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                _db.Users.Remove(user);
                await _db.SaveChangesAsync();
            }
        }
    }
}
