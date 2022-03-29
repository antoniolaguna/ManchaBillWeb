using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface IReturnService : IGenericService<Return>
    {
        Task<List<Return>> GetAllReturnsActives();
        Task LogicDelete(int id);
        Return GetByIdWithAllRelationships(int id);
    }
}
