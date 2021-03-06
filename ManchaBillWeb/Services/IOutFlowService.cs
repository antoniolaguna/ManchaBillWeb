using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface IOutFlowService : IGenericService<OutFlow>
    {
        Task<List<OutFlow>> GetAllOutFlowsActives();
        Task<List<OutFlow>> GetAllOutFlowsActives(DateTime fromDate);
        Task LogicDelete(int id);
    }
}
