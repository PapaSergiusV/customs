using System.ComponentModel.DataAnnotations;

namespace customs.ViewModels
{
    public class SignupView : SigninView
    {
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Wrong password confirmation")]
        public string PasswordConfirmation { get; set; }
    }
}
