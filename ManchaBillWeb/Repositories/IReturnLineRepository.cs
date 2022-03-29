using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Repositories
{
    public interface IReturnLineRepository : IGenericRepository<ReturnLine>
    {
        Task<List<ReturnLine>> GetAllReturnLinesActives();
        Task<List<ReturnLine>> GetAllReturnLinesActivesByReturn(int returnId);
        Task LogicDelete(int id);
        ReturnLine GetByIdWithAllRelationships(int id);
    }
}
