using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.OutputResponce;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BankAppCotroller : ControllerBase
{
    
    public IBankBL _bankBL;

    // primarily due to Dependency Injection (DI) and Loose Coupling. 
    public BankAppCotroller(IBankBL bankBL)
    {
        _bankBL = bankBL;
    }

    // Bank details
    [HttpGet("Get-Bank-details")]
    public async Task<Responce<List<BankDetails>>> GetBankDetails()
    {
        Responce<List<BankDetails>> responce = await _bankBL.GetBankDetail();
        return responce;
    }

    [HttpPost("Add-Bank-details")]
    public async Task<Responce<object>> AddBankDetails(BankDetails bankDetails)
    {
        Responce<object> responce = await _bankBL.AddBank(bankDetails);
        return responce;
    }

    [HttpPut("Update-Bank-details")]
    public async Task<Responce<object>> UpdateBankDetails(BankDetails bankDetails)
    {
        Responce<object> responce = await _bankBL.UpdateBank(bankDetails);
        return responce;
    }

    // Account Holders details
    [HttpGet("Get-AccountHolders-details")]
    public async Task<Responce<List<AccountHoldersDetails>>> GetAccountHoldersDetails()
    {
        Responce<List<AccountHoldersDetails>> responce = await _bankBL.GetAccountHoldersDetails();
        return responce;
    }

    [HttpPost("Add-AccountHolders-details")]
    public async Task<Responce<object>> AddAccountHoldersDetails(AccountHoldersDetails accountHoldersDetails)
    {
        Responce<object> responce = await _bankBL.AddAccountHolderDetails(accountHoldersDetails);
        return responce;
    }

    [HttpPut("Update-AccountHolders-details")]
    public async Task<Responce<object>> UpdateAccountHoldersDetails(AccountHoldersDetails accountHoldersDetails)
    {
        Responce<object> responce = await _bankBL.UpdateAccountHolderDetails(accountHoldersDetails);
        return responce;
    }

    // Withdraw and Deposit Amount
    [HttpPost("Withdraw-Amount")]
    public async Task<Responce<object>> WithdrawAmount(string atmCardNumber, int? cvv, string atmPin, decimal? withdrawalAmount)
    {
        Responce<object> responce = await _bankBL.WithdrawAmount(atmCardNumber, cvv, atmPin, withdrawalAmount);
        return responce;
    }

    [HttpPost("Deposit-Amount")]
    public async Task<Responce<object>> DepositAmount(string accountNumber, string accountHolderName, decimal? depositAmount)
    {
        Responce<object> responce = await _bankBL.DepositAmount(accountNumber, accountHolderName, depositAmount);
        return responce;
    }

}
