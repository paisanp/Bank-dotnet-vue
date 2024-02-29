namespace BankAPI.ViewModels
{
    public class BankModel
    {
        public Guid UserId { get; set; }
        public int amount { get; set; }
    }

    public class DepositAndWithDrawModel : BankModel
    {
    }

    public class TranferwModel : BankModel
    {
        public Guid ReceiverId { get; set; }
    }

    public class HistortTransaction
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
    }
}
