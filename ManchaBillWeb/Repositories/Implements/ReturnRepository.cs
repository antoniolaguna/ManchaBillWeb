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
    public class ReturnRepository : GenericRepository<Return>, IReturnRepository
    {
        private readonly ApplicationDbContext context;
        public ReturnRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Return>> GetAllReturnsActives()
        {
            var result = this.context.Returns.Where(r => r.Active)
                .Include(r => r.ReturnLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Size)
                .Include(r => r.ReturnLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Item)
                .ToListAsync();

            return result;
        }

        public Task LogicDelete(int id)
        {
            Return returnToLogicDelete = context.Returns.Find(id);
            returnToLogicDelete.DeleteDate = DateTime.Now;
            returnToLogicDelete.Active = false;
            context.Update(returnToLogicDelete);
            return context.SaveChangesAsync();
        }

        public Return GetByIdWithAllRelationships(int id)
        {
            return context.Set<Return>()
                .Where(s => s.Id == id)
                 .Include(r => r.ReturnLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Size)
                .Include(r => r.ReturnLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Item)
                .FirstOrDefault();
        }
    }
}
