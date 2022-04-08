using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface ICashRegisterService : IGenericService<CashRegister>
    {
        Task<List<CashRegister>> GetAllCashRegistersActives();
        CashRegister GetLastCashRegister();
        Task LogicDelete(int id);
    }
}