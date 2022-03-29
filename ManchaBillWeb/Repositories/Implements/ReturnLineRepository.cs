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
    public class ReturnLineRepository : GenericRepository<ReturnLine>, IReturnLineRepository
    {
        private readonly ApplicationDbContext context;
        public ReturnLineRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<ReturnLine>> GetAllReturnLinesActives()
        {
            var result = this.context.ReturnLines.Where(s => s.Active)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .ToListAsync();
            return result;
        }

        public Task<List<ReturnLine>> GetAllReturnLinesActivesByReturn(int returnId)
        {
            var result = this.context.ReturnLines.Where(s => s.Active)
                .Where(s => s.ReturnId == returnId)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            ReturnLine returnLineToLogicDelete = context.ReturnLines.Find(id);
            returnLineToLogicDelete.DeleteDate = DateTime.Now;
            returnLineToLogicDelete.Active = false;
            context.Update(returnLineToLogicDelete);
            return context.SaveChangesAsync();
        }

        public ReturnLine GetByIdWithAllRelationships(int id)
        {
            return context.Set<ReturnLine>()
                .Where(s => s.Id == id)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .FirstOrDefault();
        }
    }
}
