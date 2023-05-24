using System.ComponentModel.DataAnnotations;

namespace Siimple.ViewModels.AccountVm
{
    public class RegisterVm
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        [EmailAddress]
        public string Email { get; set; }
        public string Username { get; set; } = null!;
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password),Compare(nameof(Password))]
        public string ConfirmPassword { get; set;}
    }
}
