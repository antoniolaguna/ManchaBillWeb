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
    public class OutFlowRepository : GenericRepository<OutFlow>, IOutFlowRepository
    {
        private readonly ApplicationDbContext context;
        public OutFlowRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<OutFlow>> GetAllOutFlowsActives()
        {
            var listOutFlow = context.OutFlows
                .Where(x => x.Active)
                .ToListAsync();
            return listOutFlow;
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
