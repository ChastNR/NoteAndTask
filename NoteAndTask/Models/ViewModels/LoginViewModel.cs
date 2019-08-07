using System.ComponentModel.DataAnnotations;

namespace NoteAndTask.Models.ViewModels
{
    public class LoginViewModel
    {
        public string Login { get; set; }

        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
