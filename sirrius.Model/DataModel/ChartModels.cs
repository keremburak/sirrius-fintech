using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Model.DataModel
{
    public class DailyChartParameters
    {
        //public int userid { get; set; }
        public string companyid { get; set; }
        public string bankid { get; set; }
        public string currencycode { get; set; }
        public string account { get; set; }
        public string dailyrange { get; set; }
    }

    public class DailyItemModel
    {
        public DailyItemModel()
        {
            this.DailyItems = new List<DailyGeneralItem>();
        }

        public ICollection<DailyGeneralItem> DailyItems { get; set; }
    }

    public class DailyGeneralItem
    {
        public string DailyDate { get; set; }
        public decimal Total { get; set; }
    }


    public class BankAmountModel
    {
        public BankAmountModel()
        {
            CurrencyAmounts = new List<CurrencyAmountModel>();
        }

        public string BankCode { get; set; }
        public string BankName { get; set; }

        public ICollection<CurrencyAmountModel> CurrencyAmounts { get; set; }
    }

    public class CurrencyAmountModel
    {
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string TotalAmount { get; set; }
    }
}
