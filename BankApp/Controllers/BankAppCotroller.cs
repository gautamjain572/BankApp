using BusinessLayer.Interfaces;
using CommonLayer.Models;
using CommonLayer.OutputResponce;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.Controllers;

[ApiController]
[Route("api/")]
public class BankAppCotroller : ControllerBase
{
    // First
    public IBankBL _bankBL;

    // primarily due to Dependency Injection (DI) and Loose Coupling. 
    public BankAppCotroller(IBankBL bankBL)
    {
        _bankBL = bankBL;
    }

    // Bank details
    [HttpGet("bank/Get-Bank-details")]
    public async Task<Responce<List<BankDetails>>> GetBankDetails()
    {
        Responce<List<BankDetails>> responce = await _bankBL.GetBankDetail();
        return responce;
    }

    [HttpPost("bank/Add-Bank-details")]
    public async Task<Responce<object>> AddBankDetails(BankDetails bankDetails)
    {
        Responce<object> responce = await _bankBL.AddBank(bankDetails);
        return responce;
    }

    [HttpPut("bank/Update-Bank-details")]
    public async Task<Responce<object>> UpdateBankDetails(BankDetails bankDetails)
    {
        Responce<object> responce = await _bankBL.UpdateBank(bankDetails);
        return responce;
    }

    // Account Holders details
    [HttpGet("Account/Get-AccountHolders-details")]
    public async Task<Responce<List<AccountHoldersDetails>>> GetAccountHoldersDetails()
    {
        Responce<List<AccountHoldersDetails>> responce = await _bankBL.GetAccountHoldersDetails();
        return responce;
    }

    [HttpPost("Account/Add-AccountHolders-details")]
    public async Task<Responce<object>> AddAccountHoldersDetails(AccountHoldersDetails accountHoldersDetails)
    {
        Responce<object> responce = await _bankBL.AddAccountHolderDetails(accountHoldersDetails);
        return responce;
    }

    [HttpPut("Account/Update-AccountHolders-details")]
    public async Task<Responce<object>> UpdateAccountHoldersDetails(AccountHoldersDetails accountHoldersDetails)
    {
        Responce<object> responce = await _bankBL.UpdateAccountHolderDetails(accountHoldersDetails);
        return responce;
    }

    // Withdraw and Deposit Amount
    [HttpPost("Account/Withdraw-Amount")]
    public async Task<Responce<object>> WithdrawAmount(WithdrawRequest withdrawRequest)
    {
        Responce<object> responce = await _bankBL.WithdrawAmount(withdrawRequest);
        return responce;
    }

    [HttpPost("Account/Deposit-Amount")]
    public async Task<Responce<object>> DepositAmount(DepositRequest depositRequest)
    {
        Responce<object> responce = await _bankBL.DepositAmount(depositRequest);
        return responce;
    }

}
