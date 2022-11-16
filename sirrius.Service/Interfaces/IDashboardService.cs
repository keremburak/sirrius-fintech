using sirrius.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sirrius.Service.Interfaces
{
    public interface IDashboardService : IService
    {
        Task<List<BankStatement>> GetBankStatements(User user);
    }
}

