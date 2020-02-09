using customs.ViewModels;
using customs.Models;

namespace customs
{
    public interface IUserService
    {
        User SignIn(SigninView signinView);
        User SignUp(SignupView signupView);
        bool Exists(SigninView signinView);
        void Destroy(string email);
    }
}