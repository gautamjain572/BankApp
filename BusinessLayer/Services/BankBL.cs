using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.OutputResponce;
using RepositoryLayer.Interfaces;
using RepositoryLayer.ManageEFPowerTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BankBL : IBankBL

    {
        public IBankDataRL _bankDataRL;

        public BankBL(IBankDataRL bankDataRL)
        {
            _bankDataRL = bankDataRL;
        }

        //Method to add bank details
        // validatios didit work need output params in sp
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
    }
}
