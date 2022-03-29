using ManchaBillWeb.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public interface IModelRepository:IGenericRepository<Model>
    {
        Task<List<Model>> GetAllModelsActives();
        Task LogicDelete(int id);
        Task LogicDeleteByItem(int itemId);
        Task<List<Model>> GetAllModelsActivesByBarcode(string barcode);
        Model GetByIdWithAllRelationships(int id);
    }
}
