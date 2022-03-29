using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IBillService : IGenericService<Bill>
    {
        Task<List<Bill>> GetAllBillsActives();
        Task LogicDelete(int id);
        Bill GetByIdWithAllRelationships(int id);
        Bill GetBillByBillNumber(string numberBill);

    }
}
