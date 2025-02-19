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

    public BankAppCotroller(IBankBL bankBL)
    {
        _bankBL = bankBL;
    }

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
}
