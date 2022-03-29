using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IBillLineService : IGenericService<BillLine>
    {
        Task<List<BillLine>> GetAllBillLinesActives();
        Task<List<BillLine>> GetAllBillLinesActivesByBill(int idBill);
        Task LogicDelete(int id);

        BillLine GetByIdWithAllRelationships(int id);

    }
}
