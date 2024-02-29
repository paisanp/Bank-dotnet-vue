using System.ComponentModel.DataAnnotations;

namespace BankAPI.ViewModels
{
    public class LoginModel
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }

    public class RegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
    }

    public class UserModel
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassWord { get; set; }
        public int CurrentBalance { get; set; }

    }

    public class UserRespone
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public int CurrentBalance { get; set; }

    }

}
