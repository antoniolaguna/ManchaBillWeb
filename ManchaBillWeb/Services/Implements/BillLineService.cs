using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class BillLineService : GenericService<BillLine>, IBillLineService
    {
        private readonly IBillLineRepository billLineRepository;
        public BillLineService(IBillLineRepository billLineRepository) : base(billLineRepository)
        {
            this.billLineRepository = billLineRepository;
        }

        public Task<List<BillLine>> GetAllBillLinesActives()
        {
            return this.billLineRepository.GetAllBillLinesActives();
        }
        public Task<List<BillLine>> GetAllBillLinesActivesByBill(int idBill)
        {
            return this.billLineRepository.GetAllBillLinesActivesByBill(idBill);
        }

        public Task LogicDelete(int id)
        {
            return this.billLineRepository.LogicDelete(id);
        }

        public BillLine GetByIdWithAllRelationships(int id)
        {
            return this.billLineRepository.GetByIdWithAllRelationships(id);
        }
    }
}
