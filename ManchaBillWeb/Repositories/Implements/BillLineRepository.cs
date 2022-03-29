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
    public class BillLineRepository : GenericRepository<BillLine>, IBillLineRepository
    {
        private readonly ApplicationDbContext context;
        public BillLineRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<BillLine>> GetAllBillLinesActives()
        {
            var result = this.context.BillLines.Where(s => s.Active)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .ToListAsync();
            return result;
        }

        public Task<List<BillLine>> GetAllBillLinesActivesByBill(int billId)
        {
            var result = this.context.BillLines.Where(s => s.Active)
                .Where(s=>s.BillId==billId)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .ToListAsync();
            return result;
        }

        public Task LogicDelete(int id)
        {
            BillLine billLineToLogicDelete = context.BillLines.Find(id);
            billLineToLogicDelete.DeleteDate = DateTime.Now;
            billLineToLogicDelete.Active = false;
            context.Update(billLineToLogicDelete);
            return context.SaveChangesAsync();
        }

        public BillLine GetByIdWithAllRelationships(int id)
        {
            return context.Set<BillLine>()
                .Where(s => s.Id == id)
                .Include(s => s.Model)
                .ThenInclude(s => s.Item)
                .Include(s => s.Model)
                .ThenInclude(s => s.Size)
                .FirstOrDefault();
        }
    }
}
