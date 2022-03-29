using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class BillService : GenericService<Bill>, IBillService
    {
        private readonly IBillRepository billRepository;
        public BillService(IBillRepository billRepository) : base(billRepository)
        {
            this.billRepository = billRepository;
        }

        public Task<List<Bill>> GetAllBillsActives()
        {
            return this.billRepository.GetAllBillsActives();
        }

        public Task LogicDelete(int id)
        {
            return this.billRepository.LogicDelete(id);
        }

        public Bill GetByIdWithAllRelationships(int id)
        {
            return this.billRepository.GetByIdWithAllRelationships(id);
        }

        public Bill GetBillByBillNumber(string numberBill)
        {
            return this.billRepository.GetBillByBillNumber(numberBill);
        }
    }
}
