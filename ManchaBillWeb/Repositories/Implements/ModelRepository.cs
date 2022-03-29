using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Repositories.Implements
{
    public class ModelRepository:GenericRepository<Model>, IModelRepository
    {
        private readonly ApplicationDbContext context;
        public ModelRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Model>> GetAllModelsActives()
        {
            var result = this.context.Models.Where(s => s.Active).Include(s=>s.Item).Include(s=>s.Size).ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            Model modelToLogicDelete = context.Models.Find(id);
            modelToLogicDelete.DeleteDate = DateTime.Now;
            modelToLogicDelete.Active = false;
            context.Update(modelToLogicDelete);
            return context.SaveChangesAsync();
        }

        public Task LogicDeleteByItem(int itemId)
        {
            List<Model> result = this.context.Models.Where(s => s.ItemId== itemId).ToListAsync().Result;

            foreach(Model m in result)
            {
                m.DeleteDate = DateTime.Now;
                m.Active = false;
                context.Update(m);
            }
            return context.SaveChangesAsync();
        }

        public Task<List<Model>> GetAllModelsActivesByBarcode(string barcode)
        {
            var result = this.context.Models.Where(s => s.Active && s.Barcode.Equals(barcode)).Include(s => s.Item).Include(s => s.Size).ToListAsync();
            return result;
        }

        public Model GetByIdWithAllRelationships(int id)
        {
            return context.Set<Model>()
                .Where(s => s.Id == id)
                .Include(s => s.Item).ThenInclude(s => s.ItemType)
                .Include(s => s.Size)
                
                .FirstOrDefault();
        }
    }
}
