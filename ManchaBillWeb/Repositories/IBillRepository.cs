using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public interface IBillRepository : IGenericRepository<Bill>
    {
        Task<List<Bill>> GetAllBillsActives();
        Task LogicDelete(int id);
        Bill GetByIdWithAllRelationships(int id);
        Bill GetBillByBillNumber(string numberBill);
    }
}
