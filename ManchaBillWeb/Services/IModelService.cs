using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IModelService:IGenericService<Model>
    {
        Task<List<Model>> GetAllModelsActives();
        Task LogicDelete(int id);
        Task LogicDeleteByItem(int itemId);
        Task<List<Model>> GetAllModelsActivesByBarcode(string barcode);
        Model GetByIdWithAllRelationships(int id);
    }
}
