using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface IReturnLineService : IGenericService<ReturnLine>
    {
        Task<List<ReturnLine>> GetAllReturnLinesActives();
        Task<List<ReturnLine>> GetAllReturnLinesActivesByReturn(int idReturn);
        Task LogicDelete(int id);
        ReturnLine GetByIdWithAllRelationships(int id);
    }
}
