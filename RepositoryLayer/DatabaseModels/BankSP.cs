using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.DatabaseModels
{
    public class BankSP
    {
        public partial class GetAllBankDetailsResult
        {
            public int BankID { get; set; }
            public string BankName { get; set; }
            public string ISFCCode { get; set; }
            public string Branch { get; set; }
            public string BankAddress { get; set; }
            public string City { get; set; }
        }

        public partial class GetAllAccountHoldersResult
        {
            public int AccountID { get; set; }
            public string AccountHolderName { get; set; }
            public string Gender { get; set; }
            public string AccountNumber { get; set; }
            public int BankID { get; set; }
            [Column("Balance", TypeName = "decimal(18,2)")]
            public decimal? Balance { get; set; }
            public string PANCard { get; set; }
            public string AadharCard { get; set; }
            public string ATMCardNumber { get; set; }
            public int? CVV { get; set; }
            public string ATMPin { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
            public DateTime? CreatedDate { get; set; }
        }
    }
}
