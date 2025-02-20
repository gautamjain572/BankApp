using RepositoryLayer.ManageEFPowerTools;
using static RepositoryLayer.DatabaseModels.BankSP;

// Iterface RL
namespace RepositoryLayer.Interfaces
{
    public interface IBankDataRL
    {
        // bank details
        Task<List<GetAllBankDetailsResult>> GetAllBankDetailsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> InsertBankDetailsAsync(string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankAdded, OutputParameter<string> bankAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UpdateBankDetailsAsync(int? ID, string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankUpdated, OutputParameter<string> bankUpdateStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);

        // account holder details
        Task<List<DatabaseModels.BankSP.GetAllAccountHoldersResult>> GetAllAccountHoldersAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> InsertAccountHolderDetailsAsync(string AccountHolderName, string Gender, string AccountNumber, int? BankID, decimal? Balance, string PANCard, string AadharCard, string ATMCardNumber, int? CVV, string ATMPin, string PhoneNumber, string Email, string Address, OutputParameter<bool?> isAccountAdded, OutputParameter<string> accountAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UpdateAccountHolderDetailsAsync(int? AccountID, string AccountHolderName, string Gender, string AccountNumber, int? BankID, string PANCard, string AadharCard, string ATMCardNumber, int? CVV, string ATMPin, string PhoneNumber, string Email, string Address, OutputParameter<bool?> isUpdateSuccessful, OutputParameter<string> updateStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);

        // withdrawal and deposit
        Task<int> WithdrawAmountAsync(string ATMCardNumber, int? CVV, string ATMPin, decimal? WithdrawalAmount, OutputParameter<bool?> isWithdrawalSuccessful, OutputParameter<string> withdrawalStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> DepositAmountAsync(string AccountNumber, string AccountHolderName, decimal? DepositAmount, OutputParameter<bool?> isDepositSuccessful, OutputParameter<string> depositStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
    }
}
