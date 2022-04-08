using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Repositories
{
    public interface ICashRegisterRepository : IGenericRepository<CashRegister>
    {
        Task<List<CashRegister>> GetAllCashRegistersActives();
        CashRegister GetLastCashRegister();
        Task LogicDelete(int id);
    }
}
