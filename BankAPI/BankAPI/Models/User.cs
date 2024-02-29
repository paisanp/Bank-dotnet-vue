using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string UserName { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string PassWord { get; set; }
        [Required]
        public int CurrentBalance { get; set; } 
    }
}
