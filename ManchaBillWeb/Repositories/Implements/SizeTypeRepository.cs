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
    public class SizeTypeRepository : GenericRepository<SizeType>, ISizeTypeRepository
    {
        private readonly ApplicationDbContext context;
        public SizeTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<SizeType>> GetAllSizeTypesActives()
        {
            var listSizeType = context.SizeTypes
                .Where(x => x.Active)
                .ToListAsync();
            return listSizeType;
        }

        public async Task LogicDelete(int id)
        {
            var entity = await GetById(id);
            entity.DeleteDate = DateTime.Now;
            entity.Active = false;
            context.Update(entity);
            await context.SaveChangesAsync();
        }

       
    }
}
