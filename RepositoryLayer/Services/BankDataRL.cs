using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DataContext;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ManageEFPowerTools;
using static RepositoryLayer.DatabaseModels.BankSP;

// Service RL
namespace RepositoryLayer.Services
{
    public class BankDataRL : IBankDataRL
    {
        public BankDataCotext _bankDataCotext;

        // from Data Context
        public BankDataRL(BankDataCotext bankDataContext)
        {
            _bankDataCotext = bankDataContext;
        }

        // get all bank details
        public virtual async Task<List<GetAllBankDetailsResult>> GetAllBankDetailsAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.SqlQueryAsync<GetAllBankDetailsResult>("EXEC @returnValue = [dbo].[GetAllBankDetails]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
        // insert bank details
        public virtual async Task<int> InsertBankDetailsAsync(string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankAdded, OutputParameter<string> bankAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisBankAdded = new SqlParameter
            {
                ParameterName = "isBankAdded",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isBankAdded?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameterbankAddedStatus = new SqlParameter
            {
                ParameterName = "bankAddedStatus",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = bankAddedStatus?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "BankName",
                    Size = 255,
                    Value = BankName ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "ISFCCode",
                    Size = 255,
                    Value = ISFCCode ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Branch",
                    Size = 255,
                    Value = Branch ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "BankAddress",
                    Size = 255,
                    Value = BankAddress ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "City",
                    Size = 255,
                    Value = City ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterisBankAdded,
                parameterbankAddedStatus,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[InsertBankDetails] @BankName = @BankName, @ISFCCode = @ISFCCode, @Branch = @Branch, @BankAddress = @BankAddress, @City = @City, @isBankAdded = @isBankAdded OUTPUT, @bankAddedStatus = @bankAddedStatus OUTPUT", sqlParameters, cancellationToken);

            isBankAdded?.SetValue(parameterisBankAdded.Value);
            bankAddedStatus?.SetValue(parameterbankAddedStatus.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
        // update bank details
        public virtual async Task<int> UpdateBankDetailsAsync(int? ID, string BankName, string ISFCCode, string Branch, string BankAddress, string City, OutputParameter<bool?> isBankUpdated, OutputParameter<string> bankUpdateStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisBankUpdated = new SqlParameter
            {
                ParameterName = "isBankUpdated",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isBankUpdated?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameterbankUpdateStatus = new SqlParameter
            {
                ParameterName = "bankUpdateStatus",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = bankUpdateStatus?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "ID",
                    Value = ID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "BankName",
                    Size = 255,
                    Value = BankName ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "ISFCCode",
                    Size = 255,
                    Value = ISFCCode ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Branch",
                    Size = 255,
                    Value = Branch ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "BankAddress",
                    Size = 255,
                    Value = BankAddress ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "City",
                    Size = 255,
                    Value = City ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterisBankUpdated,
                parameterbankUpdateStatus,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[UpdateBankDetails] @ID = @ID, @BankName = @BankName, @ISFCCode = @ISFCCode, @Branch = @Branch, @BankAddress = @BankAddress, @City = @City, @isBankUpdated = @isBankUpdated OUTPUT, @bankUpdateStatus = @bankUpdateStatus OUTPUT", sqlParameters, cancellationToken);

            isBankUpdated?.SetValue(parameterisBankUpdated.Value);
            bankUpdateStatus?.SetValue(parameterbankUpdateStatus.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        // get all account holders
        public virtual async Task<List<GetAllAccountHoldersResult>> GetAllAccountHoldersAsync(OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.SqlQueryAsync<DatabaseModels.BankSP.GetAllAccountHoldersResult>("EXEC @returnValue = [dbo].[GetAllAccountHolders]", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
        // insert account holder details
        public virtual async Task<int> InsertAccountHolderDetailsAsync(string AccountHolderName, string Gender, string AccountNumber, int? BankID, decimal? Balance, string PANCard, string AadharCard, string ATMCardNumber, int? CVV, string? ATMPin, string PhoneNumber, string Email, string Address, OutputParameter<bool?> isAccountAdded, OutputParameter<string> accountAddedStatus, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisAccountAdded = new SqlParameter
            {
                ParameterName = "isAccountAdded",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isAccountAdded?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameteraccountAddedStatus = new SqlParameter
            {
                ParameterName = "accountAddedStatus",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = accountAddedStatus?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "AccountHolderName",
                    Size = 255,
                    Value = AccountHolderName ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Gender",
                    Size = 10,
                    Value = Gender ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "AccountNumber",
                    Size = 50,
                    Value = AccountNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "BankID",
                    Value = BankID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "Balance",
                    Precision = 18,
                    Scale = 2,
                    Value = Balance ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Decimal,
                },
                new SqlParameter
                {
                    ParameterName = "PANCard",
                    Size = 10,
                    Value = PANCard ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "AadharCard",
                    Size = 12,
                    Value = AadharCard ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "ATMCardNumber",
                    Size = 20,
                    Value = ATMCardNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "CVV",
                    Value = CVV ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "ATMPin",
                    Size = 4,
                    Value = ATMPin ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Char,
                },
                new SqlParameter
                {
                    ParameterName = "PhoneNumber",
                    Size = 20,
                    Value = PhoneNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Email",
                    Size = 255,
                    Value = Email ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Address",
                    Size = 500,
                    Value = Address ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterisAccountAdded,
                parameteraccountAddedStatus,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[InsertAccountHolderDetails] @AccountHolderName = @AccountHolderName, @Gender = @Gender, @AccountNumber = @AccountNumber, @BankID = @BankID, @Balance = @Balance, @PANCard = @PANCard, @AadharCard = @AadharCard, @ATMCardNumber = @ATMCardNumber, @CVV = @CVV, @ATMPin = @ATMPin, @PhoneNumber = @PhoneNumber, @Email = @Email, @Address = @Address, @isAccountAdded = @isAccountAdded OUTPUT, @accountAddedStatus = @accountAddedStatus OUTPUT", sqlParameters, cancellationToken);

            isAccountAdded?.SetValue(parameterisAccountAdded.Value);
            accountAddedStatus?.SetValue(parameteraccountAddedStatus.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
        // update account holder details
        public virtual async Task<int> UpdateAccountHolderDetailsAsync(int? AccountID, string AccountHolderName, string Gender, string AccountNumber, int? BankID, string PANCard, string AadharCard, string ATMCardNumber, int? CVV, string ATMPin, string PhoneNumber, string Email, string Address, OutputParameter<bool?> isUpdateSuccessful, OutputParameter<string> updateStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisUpdateSuccessful = new SqlParameter
            {
                ParameterName = "isUpdateSuccessful",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isUpdateSuccessful?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameterupdateStatusMessage = new SqlParameter
            {
                ParameterName = "updateStatusMessage",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = updateStatusMessage?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "AccountID",
                    Value = AccountID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "AccountHolderName",
                    Size = 255,
                    Value = AccountHolderName ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Gender",
                    Size = 10,
                    Value = Gender ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "AccountNumber",
                    Size = 50,
                    Value = AccountNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "BankID",
                    Value = BankID ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "PANCard",
                    Size = 10,
                    Value = PANCard ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "AadharCard",
                    Size = 12,
                    Value = AadharCard ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "ATMCardNumber",
                    Size = 20,
                    Value = ATMCardNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "CVV",
                    Value = CVV ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "ATMPin",
                    Size = 4,
                    Value = ATMPin ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Char,
                },
                new SqlParameter
                {
                    ParameterName = "PhoneNumber",
                    Size = 20,
                    Value = PhoneNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Email",
                    Size = 255,
                    Value = Email ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "Address",
                    Size = 500,
                    Value = Address ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                parameterisUpdateSuccessful,
                parameterupdateStatusMessage,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[UpdateAccountHolderDetails] @AccountID = @AccountID, @AccountHolderName = @AccountHolderName, @Gender = @Gender, @AccountNumber = @AccountNumber, @BankID = @BankID, @PANCard = @PANCard, @AadharCard = @AadharCard, @ATMCardNumber = @ATMCardNumber, @CVV = @CVV, @ATMPin = @ATMPin, @PhoneNumber = @PhoneNumber, @Email = @Email, @Address = @Address, @isUpdateSuccessful = @isUpdateSuccessful OUTPUT, @updateStatusMessage = @updateStatusMessage OUTPUT", sqlParameters, cancellationToken);

            isUpdateSuccessful?.SetValue(parameterisUpdateSuccessful.Value);
            updateStatusMessage?.SetValue(parameterupdateStatusMessage.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }

        // withdraw amount
        public virtual async Task<int> WithdrawAmountAsync(string ATMCardNumber, int? CVV, string ATMPin, decimal? WithdrawalAmount, OutputParameter<bool?> isWithdrawalSuccessful, OutputParameter<string> withdrawalStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisWithdrawalSuccessful = new SqlParameter
            {
                ParameterName = "isWithdrawalSuccessful",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isWithdrawalSuccessful?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameterwithdrawalStatusMessage = new SqlParameter
            {
                ParameterName = "withdrawalStatusMessage",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = withdrawalStatusMessage?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "ATMCardNumber",
                    Size = 20,
                    Value = ATMCardNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "CVV",
                    Value = CVV ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                new SqlParameter
                {
                    ParameterName = "ATMPin",
                    Size = 4,
                    Value = ATMPin ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Char,
                },
                new SqlParameter
                {
                    ParameterName = "WithdrawalAmount",
                    Precision = 18,
                    Scale = 2,
                    Value = WithdrawalAmount ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Decimal,
                },
                parameterisWithdrawalSuccessful,
                parameterwithdrawalStatusMessage,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[WithdrawAmount] @ATMCardNumber = @ATMCardNumber, @CVV = @CVV, @ATMPin = @ATMPin, @WithdrawalAmount = @WithdrawalAmount, @isWithdrawalSuccessful = @isWithdrawalSuccessful OUTPUT, @withdrawalStatusMessage = @withdrawalStatusMessage OUTPUT", sqlParameters, cancellationToken);

            isWithdrawalSuccessful?.SetValue(parameterisWithdrawalSuccessful.Value);
            withdrawalStatusMessage?.SetValue(parameterwithdrawalStatusMessage.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
        // deposit amount
        public virtual async Task<int> DepositAmountAsync(string AccountNumber, string AccountHolderName, decimal? DepositAmount, OutputParameter<bool?> isDepositSuccessful, OutputParameter<string> depositStatusMessage, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterisDepositSuccessful = new SqlParameter
            {
                ParameterName = "isDepositSuccessful",
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = isDepositSuccessful?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.Bit,
            };
            var parameterdepositStatusMessage = new SqlParameter
            {
                ParameterName = "depositStatusMessage",
                Size = 400,
                Direction = System.Data.ParameterDirection.InputOutput,
                Value = depositStatusMessage?._value ?? Convert.DBNull,
                SqlDbType = System.Data.SqlDbType.VarChar,
            };
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new[]
            {
                new SqlParameter
                {
                    ParameterName = "AccountNumber",
                    Size = 50,
                    Value = AccountNumber ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "AccountHolderName",
                    Size = 255,
                    Value = AccountHolderName ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.VarChar,
                },
                new SqlParameter
                {
                    ParameterName = "DepositAmount",
                    Precision = 18,
                    Scale = 2,
                    Value = DepositAmount ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Decimal,
                },
                parameterisDepositSuccessful,
                parameterdepositStatusMessage,
                parameterreturnValue,
            };
            var _ = await _bankDataCotext.Database.ExecuteSqlRawAsync("EXEC @returnValue = [dbo].[DepositAmount] @AccountNumber = @AccountNumber, @AccountHolderName = @AccountHolderName, @DepositAmount = @DepositAmount, @isDepositSuccessful = @isDepositSuccessful OUTPUT, @depositStatusMessage = @depositStatusMessage OUTPUT", sqlParameters, cancellationToken);

            isDepositSuccessful?.SetValue(parameterisDepositSuccessful.Value);
            depositStatusMessage?.SetValue(parameterdepositStatusMessage.Value);
            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}
