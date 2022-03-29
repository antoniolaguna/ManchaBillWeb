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
    public class BillRepository : GenericRepository<Bill>, IBillRepository
    {
        private readonly ApplicationDbContext context;
        public BillRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Bill>> GetAllBillsActives()
        {
            var result = this.context.Bills.Where(bill => bill.Active)
                .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Size)
                .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Item)
                .Include(bill=>bill.PaymentType)
                .ToListAsync();

            return result;
        }

        public Task LogicDelete(int id)
        {
            Bill billToLogicDelete = context.Bills.Find(id);
            billToLogicDelete.DeleteDate = DateTime.Now;
            billToLogicDelete.Active = false;
            context.Update(billToLogicDelete);
            return context.SaveChangesAsync();
        }

        public Bill GetByIdWithAllRelationships(int id)
        {
            return context.Set<Bill>()
                .Where(s => s.Id == id)
                 .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Size)
                .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Item)
                .FirstOrDefault();
        }

        public Bill GetBillByBillNumber(string billNumber)
        {
            return this.context.Set<Bill>().Where(bill => bill.Active && billNumber.Equals(bill.SeriesBill+bill.YearBill.ToString()+bill.NumberBill))
                .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Size)
                .Include(bill => bill.BillLines)
                .ThenInclude(line => line.Model)
                .ThenInclude(model => model.Item)
                .Include(bill => bill.PaymentType)
                .First();
        }
    }
}
