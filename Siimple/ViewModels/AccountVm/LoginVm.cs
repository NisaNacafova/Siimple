using System.ComponentModel.DataAnnotations;

namespace Siimple.ViewModels.AccountVm
{
    public class LoginVm
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
