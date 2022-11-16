using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Model.DataModel
{
    public class DashboardExtraViewModel
    {
        public DashboardExtraViewModel()
        {
            this.AmountsByBank = new List<BankAmountModel>();
        }

        public string Income { get; set; }
        public string Outgoing { get; set; }
        public ICollection<BankAmountModel> AmountsByBank { get; set; }
    }
}
