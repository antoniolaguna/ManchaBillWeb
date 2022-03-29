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
    public class SizeRepository : GenericRepository<Size>,ISizeRepository
    {
        private readonly ApplicationDbContext context;
        public SizeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Size>> GetAllSizesActives()
        {
            var result = this.context.Sizes.Where(s => s.Active).Include(s=>s.SizeType).ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            Size sizeToLogicDelete = context.Sizes.Find(id);
            sizeToLogicDelete.DeleteDate= DateTime.Now;
            sizeToLogicDelete.Active = false;
            context.Update(sizeToLogicDelete);
            return context.SaveChangesAsync();
        }

        public Task<List<Size>> GetSizesBySizeType(int sizeTypeID)
        {
            var result = this.context.Sizes.Where(s => s.SizeTypeId== sizeTypeID).ToListAsync();
            return result;
        }
    }
}
