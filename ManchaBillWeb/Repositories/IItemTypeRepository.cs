using ManchaBillWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public interface IItemTypeRepository:IGenericRepository<ItemType>
    {
        Task<List<ItemType>> GetAllItemTypesActives();
        Task LogicDelete(int id);
    }
}
