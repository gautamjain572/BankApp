using CommonLayer.Models;
using CommonLayer.OutputResponce;

// Interface BL
namespace BusinessLayer.Interfaces
{
    public interface IBankBL
    {
        // Bank Details Interfaces
        Task<Responce<List<BankDetails>>> GetBankDetail();
        Task<Responce<object>> AddBank(BankDetails bankDetails);
        Task<Responce<object>> UpdateBank(BankDetails bankDetails);

        // Account Holders Details Interfaces
        Task<Responce<List<AccountHoldersDetails>>> GetAccountHoldersDetails();
        Task<Responce<object>> AddAccountHolderDetails(AccountHoldersDetails accountHoldersDetails);
        Task<Responce<object>> UpdateAccountHolderDetails(AccountHoldersDetails accountHoldersDetails);

        // Withdraw and Deposit Amount Interfaces
        Task<Responce<object>> WithdrawAmount(string atmCardNumber, int? cvv, string atmPin, decimal? withdrawalAmount);
        Task<Responce<object>> DepositAmount(string accountNumber, string accountHolderName, decimal? depositAmount);
    }
}
