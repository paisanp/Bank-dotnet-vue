using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
    public class Transaction
    {
        [Key]
        public Guid TransactionId { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public Guid SenderId { get; set; }
        [Required]
        public Guid receiverId { get; set; }
        [Required]
        public string Amount { get; set; }
        [Required]
        public string Remain { get; set; }
        [Required]
        public DateTime Date { get; set; }


    }
}
