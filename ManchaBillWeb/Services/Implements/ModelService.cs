using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services.Implements
{
    public class ModelService : GenericService<Model>, IModelService
    {
        private readonly IModelRepository modelRepository;
        public ModelService(IModelRepository modelRepository) : base(modelRepository)
        {
            this.modelRepository = modelRepository;
        }

        public Task<List<Model>> GetAllModelsActives()
        {
            return this.modelRepository.GetAllModelsActives();
        }

        public Task LogicDelete(int id)
        {
            return this.modelRepository.LogicDelete(id);
        }

        public Task LogicDeleteByItem(int itemId)
        {
            return this.modelRepository.LogicDeleteByItem(itemId);
        }
        public Task<List<Model>> GetAllModelsActivesByBarcode(string barcode)
        {
            return this.modelRepository.GetAllModelsActivesByBarcode(barcode);
        }

        public Model GetByIdWithAllRelationships(int id)
        {
            return this.modelRepository.GetByIdWithAllRelationships(id);
        }

    }
}
