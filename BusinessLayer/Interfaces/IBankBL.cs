using CommonLayer.Models;
using CommonLayer.OutputResponce;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interfaces
{
    public interface IBankBL
    {
        Task<Responce<List<BankDetails>>> GetBankDetail();

        Task<Responce<object>> AddBank(BankDetails bankDetails);

        Task<Responce<object>> UpdateBank(BankDetails bankDetails);

        Task<Responce<List<AccountHoldersDetails>>> GetAccountHoldersDetails();

    }
}
