namespace Kuna.Client {
    public class UserWallet {

        public string Currency { get; set; }

        public decimal Balance { get; set; }

        public decimal AvailableBalance { get; set; }

        public decimal Locked => Balance - AvailableBalance;

   }
}