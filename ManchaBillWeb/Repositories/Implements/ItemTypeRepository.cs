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
    public class ItemTypeRepository: GenericRepository<ItemType>, IItemTypeRepository
    {
        private readonly ApplicationDbContext context;
        public ItemTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<ItemType>> GetAllItemTypesActives()
        {
            var result = this.context.ItemTypes.Where(s => s.Active).Include(s=>s.SizeType).ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            ItemType itemTypeToLogicDelete = context.ItemTypes.Find(id);
            itemTypeToLogicDelete.DeleteDate = DateTime.Now;
            itemTypeToLogicDelete.Active = false;
            context.Update(itemTypeToLogicDelete);
            return context.SaveChangesAsync();
        }
    }
}
