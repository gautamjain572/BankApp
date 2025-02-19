using RepositoryLayer.ManageEFPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RepositoryLayer.DatabaseModels.BankSP;

namespace RepositoryLayer.Interfaces
{
    public interface IBankDataRL
    {
        Task<List<GetAllBankDetailsResult>> GetAllBankDetailsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> InsertBankDetailsAsync(string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankAdded, OutputParameter<string> bankAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> UpdateBankDetailsAsync(int? ID, string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankUpdated, OutputParameter<string> bankUpdateStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);

        Task<List<DatabaseModels.BankSP.GetAllAccountHoldersResult>> GetAllAccountHoldersAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);
        Task<int> InsertAccountHolderDetailsAsync(string AccountHolderName, string Gender, string AccountNumber, int? BankID, decimal? Balance, string PANCard, string AadharCard, string ATMCardNumber, int? CVV, string ATMPin, string PhoneNumber, string Email, string Address, OutputParameter<bool?> isAccountAdded, OutputParameter<string> accountAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default);

    }
}
