
// Models for Business Layer Methods
namespace CommonLayer.Models
{
    // Bank Details
    public class BankDetails
    {
        public int? BankID { get; set; }
        public string? BankName { get; set; }
        public string? ISFCCode { get; set; }
        public string? Branch { get; set; }
        public string? BankAddress { get; set; }
        public string? City { get; set; }
    }
    // Account Holders Details
    public class AccountHoldersDetails
    {
        public int AccountID { get; set; }
        public string? AccountHolderName { get; set; }
        public string? Gender { get; set; }
        public string? AccountNumber { get; set; }
        public int? BankID { get; set; }
        public decimal? Balance { get; set; }
        public string? PANCard { get; set; }
        public string? AadharCard { get; set; }
        public string? ATMCardNumber { get; set; }
        public int? CVV { get; set; }
        public string? ATMPin { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
    // Deposit
    public class DepositRequest
    {
        public string? accountNumber { get; set; }
        public string? accountHolderName { get; set; }
        public decimal? depositAmount { get; set; }
    }
    // withdrawl
    public class WithdrawRequest
    {
        public string? atmCardNumber { get; set; }
        public int? cvv { get; set; }
        public string? atmPin { get; set; }
        public decimal? withdrawalAmount { get; set; }
    }

}
