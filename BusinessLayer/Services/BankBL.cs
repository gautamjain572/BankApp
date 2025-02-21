using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.OutputResponce;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ManageEFPowerTools;

// Service BL
namespace BusinessLayer.Services
{
    public class BankBL : IBankBL
    {
        //Second
        public IBankDataRL _bankDataRL;

        // from Repository Layer
        public BankBL(IBankDataRL bankDataRL)
        {
            _bankDataRL = bankDataRL;
        }

        //Method to get all bank details
        public async Task<Responce<List<BankDetails>>> GetBankDetail()
        {
            Responce<List<BankDetails>> responce = new Responce<List<BankDetails>>();
            try
            {
                List<BankDetails> bankDetails = new List<BankDetails>();
                var result = await _bankDataRL.GetAllBankDetailsAsync();
                foreach (var item in result)
                {
                    bankDetails.Add(new BankDetails
                    {
                        BankID = item.BankID,
                        BankName = item.BankName,
                        ISFCCode = item.ISFCCode,
                        Branch = item.Branch,
                        BankAddress = item.BankAddress,
                        City = item.City
                    });
                }
                responce.Suceess = true;
                responce.Message = "Bank Details Fetched Successfully";
                responce.Data = bankDetails;
                return responce;
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = ex.Message;
                responce.Data = null;
                return responce;
            }
        }
        //Method to add bank details
        public async Task<Responce<object>> AddBank(BankDetails bankDetails)
        {
            Responce<object> responce = new Responce<object>();

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(bankDetails.BankName) ||
                    string.IsNullOrWhiteSpace(bankDetails.ISFCCode) ||
                    string.IsNullOrWhiteSpace(bankDetails.Branch) ||
                    string.IsNullOrWhiteSpace(bankDetails.BankAddress) ||
                    string.IsNullOrWhiteSpace(bankDetails.City))
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required.";
                    return responce;
                }

                // Output parameters
                var isBankAdded = new OutputParameter<bool?>();
                var bankAddedStatus = new OutputParameter<string>();

                // Insert bank details
                var result = await _bankDataRL.InsertBankDetailsAsync(
                    bankDetails.BankName,
                    bankDetails.ISFCCode,
                    bankDetails.Branch,
                    bankDetails.BankAddress,
                    bankDetails.City,
                    isBankAdded,
                    bankAddedStatus
                );

                if (isBankAdded.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Bank Details Added Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = bankAddedStatus.Value ?? "Failed to add bank details.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }
        //Method to update bank details
        public async Task<Responce<object>> UpdateBank(BankDetails bankDetails)
        {
            Responce<object> responce = new Responce<object>();

            try
            {
                // Validate required fields
                if (bankDetails.BankID <= 0 ||
                    string.IsNullOrWhiteSpace(bankDetails.BankName) ||
                    string.IsNullOrWhiteSpace(bankDetails.ISFCCode) ||
                    string.IsNullOrWhiteSpace(bankDetails.Branch) ||
                    string.IsNullOrWhiteSpace(bankDetails.BankAddress) ||
                    string.IsNullOrWhiteSpace(bankDetails.City))
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required.";
                    return responce;
                }

                // Output parameters
                var isBankUpdated = new OutputParameter<bool?>();
                var bankUpdateStatus = new OutputParameter<string>();

                // Update bank details
                var result = await _bankDataRL.UpdateBankDetailsAsync(
                    bankDetails.BankID,
                    bankDetails.BankName,
                    bankDetails.ISFCCode,
                    bankDetails.Branch,
                    bankDetails.BankAddress,
                    bankDetails.City,
                    isBankUpdated,
                    bankUpdateStatus
                );

                if (isBankUpdated.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Bank Details Updated Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = bankUpdateStatus.Value ?? "Failed to update bank details.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }

        // Method to get all account holders details
        public async Task<Responce<List<AccountHoldersDetails>>> GetAccountHoldersDetails()
        {
            Responce<List<AccountHoldersDetails>> responce = new Responce<List<AccountHoldersDetails>>();
            try
            {
                List<AccountHoldersDetails> accountHoldersDetails = new List<AccountHoldersDetails>();
                var result = await _bankDataRL.GetAllAccountHoldersAsync();
                foreach (var item in result)
                {
                    accountHoldersDetails.Add(new AccountHoldersDetails
                    {
                        AccountID = item.AccountID,
                        AccountHolderName = item.AccountHolderName,
                        Gender = item.Gender,
                        AccountNumber = item.AccountNumber,
                        BankID = item.BankID,
                        Balance = item.Balance,
                        PANCard = item.PANCard,
                        AadharCard = item.AadharCard,
                        ATMCardNumber = item.ATMCardNumber,
                        CVV = item.CVV,
                        ATMPin = item.ATMPin,
                        PhoneNumber = item.PhoneNumber,
                        Email = item.Email,
                        Address = item.Address,
                        CreatedDate = item.CreatedDate
                    });
                }
                responce.Suceess = true;
                responce.Message = "Account Holders Details Fetched Successfully";
                responce.Data = accountHoldersDetails;
                return responce;
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = ex.Message;
                responce.Data = null;
                return responce;
            }
        }
        // Method to add account holder details
        public async Task<Responce<object>> AddAccountHolderDetails(AccountHoldersDetails accountHoldersDetails)
        {
            Responce<object> responce = new Responce<object>();
            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(accountHoldersDetails.AccountHolderName) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Gender) ||
                    accountHoldersDetails.BankID <= 0 ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.PANCard) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.AadharCard) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Email) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Address))
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required.";
                    return responce;
                }
                // CVV Validation - must be exactly 3 digits
                if (accountHoldersDetails.CVV == null || accountHoldersDetails.CVV?.ToString().Length != 3)
                {
                    responce.Suceess = false;
                    responce.Message = "CVV must be a 3-digit number.";
                    return responce;
                }
                // Set default ATM pin
                accountHoldersDetails.ATMPin = "1234";
                // Generate unique Account Number
                accountHoldersDetails.AccountNumber = GenerateUniqueAccountNumber();
                // Validate minimum balance
                if (accountHoldersDetails.Balance < 2000)
                {
                    responce.Suceess = false;
                    responce.Message = "Minimum balance should be 2000.";
                    return responce;
                }
                // Output parameters
                var isAccountAdded = new OutputParameter<bool?>();
                var accountAddedStatus = new OutputParameter<string>();
                // Insert account holder details
                var result = await _bankDataRL.InsertAccountHolderDetailsAsync(
                    accountHoldersDetails.AccountHolderName,
                    accountHoldersDetails.Gender,
                    accountHoldersDetails.AccountNumber,
                    accountHoldersDetails.BankID,
                    accountHoldersDetails.Balance,
                    accountHoldersDetails.PANCard,
                    accountHoldersDetails.AadharCard,
                    accountHoldersDetails.ATMCardNumber,
                    accountHoldersDetails.CVV,
                    accountHoldersDetails.ATMPin,
                    accountHoldersDetails.PhoneNumber,
                    accountHoldersDetails.Email,
                    accountHoldersDetails.Address,
                    isAccountAdded,
                    accountAddedStatus
                );
                if (isAccountAdded.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Account Holder Details Added Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = accountAddedStatus.Value ?? "Failed to add account holder details.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }
        // Helper method to generate a unique account number
        private string GenerateUniqueAccountNumber()
        {
            Random random = new Random();
            return "ACC" + random.Next(100000000, 999999999).ToString(); // 9-digit random number prefixed with "ACC"
        }
        // Method to update account holder details
        public async Task<Responce<object>> UpdateAccountHolderDetails(AccountHoldersDetails accountHoldersDetails)
        {
            Responce<object> responce = new Responce<object>();

            try
            {
                // Validate required fields
                if (accountHoldersDetails.AccountID <= 0 ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.AccountHolderName) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Gender) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.AccountNumber) ||
                    accountHoldersDetails.BankID <= 0 ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.PANCard) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.AadharCard) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.PhoneNumber) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Email) ||
                    string.IsNullOrWhiteSpace(accountHoldersDetails.Address))
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required.";
                    return responce;
                }

                // CVV Validation - must be exactly 3 digits
                if (accountHoldersDetails.CVV == null || accountHoldersDetails.CVV?.ToString().Length != 3)
                {
                    responce.Suceess = false;
                    responce.Message = "CVV must be a 3-digit number.";
                    return responce;
                }

                // Output parameters
                var isUpdateSuccessful = new OutputParameter<bool?>();
                var updateStatusMessage = new OutputParameter<string>();

                // Update account holder details
                var result = await _bankDataRL.UpdateAccountHolderDetailsAsync(
                    accountHoldersDetails.AccountID,
                    accountHoldersDetails.AccountHolderName,
                    accountHoldersDetails.Gender,
                    accountHoldersDetails.AccountNumber,
                    accountHoldersDetails.BankID,
                    accountHoldersDetails.PANCard,
                    accountHoldersDetails.AadharCard,
                    accountHoldersDetails.ATMCardNumber,
                    accountHoldersDetails.CVV,
                    accountHoldersDetails.ATMPin,
                    accountHoldersDetails.PhoneNumber,
                    accountHoldersDetails.Email,
                    accountHoldersDetails.Address,
                    isUpdateSuccessful,
                    updateStatusMessage
                );

                if (isUpdateSuccessful.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Account Holder Details Updated Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = updateStatusMessage.Value ?? "Failed to update account holder details.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }

        // Method to withdraw amount
        public async Task<Responce<object>> WithdrawAmount(string atmCardNumber, int? cvv, string atmPin, decimal? withdrawalAmount)
        {
            Responce<object> responce = new Responce<object>();

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(atmCardNumber) || cvv == null || string.IsNullOrWhiteSpace(atmPin) || withdrawalAmount == null || withdrawalAmount <= 0)
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required and withdrawal amount must be greater than zero.";
                    return responce;
                }

                // Output parameters
                var isWithdrawalSuccessful = new OutputParameter<bool?>();
                var withdrawalStatusMessage = new OutputParameter<string>();

                // Withdraw amount
                var result = await _bankDataRL.WithdrawAmountAsync(
                    atmCardNumber,
                    cvv,
                    atmPin,
                    withdrawalAmount,
                    isWithdrawalSuccessful,
                    withdrawalStatusMessage
                );

                if (isWithdrawalSuccessful.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Amount Withdrawn Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = withdrawalStatusMessage.Value ?? "Failed to withdraw amount.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }
        // Method to deposit amount
        public async Task<Responce<object>> DepositAmount(string accountNumber, string accountHolderName, decimal? depositAmount)
        {
            Responce<object> responce = new Responce<object>();

            try
            {
                // Validate required fields
                if (string.IsNullOrWhiteSpace(accountNumber) || string.IsNullOrWhiteSpace(accountHolderName) || depositAmount == null || depositAmount <= 100)
                {
                    responce.Suceess = false;
                    responce.Message = "All fields are required and deposit amount must be greater than 100.";
                    return responce;
                }

                // Output parameters
                var isDepositSuccessful = new OutputParameter<bool?>();
                var depositStatusMessage = new OutputParameter<string>();

                // Deposit amount
                var result = await _bankDataRL.DepositAmountAsync(
                    accountNumber,
                    accountHolderName,
                    depositAmount,
                    isDepositSuccessful,
                    depositStatusMessage
                );

                if (isDepositSuccessful.Value == true)
                {
                    responce.Suceess = true;
                    responce.Message = "Amount Deposited Successfully";
                    responce.Data = result;
                }
                else
                {
                    responce.Suceess = false;
                    responce.Message = depositStatusMessage.Value ?? "Failed to deposit amount.";
                    responce.Data = null;
                }
            }
            catch (Exception ex)
            {
                responce.Suceess = false;
                responce.Message = "An error occurred: " + ex.Message;
                responce.Data = null;
            }
            return responce;
        }
    }
}
