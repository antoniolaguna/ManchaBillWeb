using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Repositories
{
    public interface IBillLineRepository : IGenericRepository<BillLine>
    {
        Task<List<BillLine>> GetAllBillLinesActives();
        Task<List<BillLine>> GetAllBillLinesActivesByBill(int billId);
        Task LogicDelete(int id);
        BillLine GetByIdWithAllRelationships(int id);
    }
}
