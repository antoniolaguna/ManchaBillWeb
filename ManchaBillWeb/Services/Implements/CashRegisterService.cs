using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class CashRegisterService : GenericService<CashRegister>, ICashRegisterService
    {
        private readonly ICashRegisterRepository cashRegisterRepository;

        public CashRegisterService(ICashRegisterRepository cashRegisterRepository) : base(cashRegisterRepository)
        {
            this.cashRegisterRepository = cashRegisterRepository;
        }

        public Task<List<CashRegister>> GetAllCashRegistersActives()
        {
            return cashRegisterRepository.GetAllCashRegistersActives();
        }

        public CashRegister GetLastCashRegister()
        {
            return cashRegisterRepository.GetLastCashRegister();
        }

        public Task LogicDelete(int id)
        {
            return cashRegisterRepository.LogicDelete(id);
        }
    }
}
