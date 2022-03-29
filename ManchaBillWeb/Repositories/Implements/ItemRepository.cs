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
    public class ItemRepository : GenericRepository<Item>, IItemRepository
    {
        private readonly ApplicationDbContext context;
        public ItemRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Item>> GetAllItemsActives()
        {
            var result = this.context.Items.Where(s => s.Active)
                .Include(s=>s.ItemType)
                .Include(s=>s.Supplier)
                .Include(s=>s.Season)
                .ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            Item itemToLogicDelete = context.Items.Find(id);
            itemToLogicDelete.DeleteDate = DateTime.Now;
            itemToLogicDelete.Active = false;
            context.Update(itemToLogicDelete);
            return context.SaveChangesAsync();
        }

        public Item GetByIdWithAllRelationships(int id)
        {
            return context.Set<Item>()
                .Where(s => s.Id == id)
                .Include(s => s.ItemType)
                .Include(s => s.Supplier)
                .Include(s => s.Season)
                .Include(s => s.Models.Where(s=>s.Active))
                .ThenInclude(s => s.Size)
                .FirstOrDefault();
        }
    }
}
