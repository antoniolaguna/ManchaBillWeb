using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface ISeasonService : IGenericService<Season>
    {
        Task<List<Season>> GetAllSeasonsActives();
        Task LogicDelete(int id);
    }
}