using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IParameterService : IGenericService<Parameter>
    {
        Task<List<Parameter>> GetAllParametersActives();
        Task LogicDelete(int id);
        Parameter GetParameterByName(string name);

    }
}
