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
    public class CashRegisterRepository : GenericRepository<CashRegister>, ICashRegisterRepository
    {
        private readonly ApplicationDbContext context;
        public CashRegisterRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<CashRegister>> GetAllCashRegistersActives()
        {
            var listCashRegister = context.CashRegisters
                .Where(x => x.Active)
                .ToListAsync();
            return listCashRegister;
        }

       

        public CashRegister GetLastCashRegister()
        {
            var lastCashRegister = context.CashRegisters
                .Where(x => x.Active)
                .OrderByDescending(c => c.ClosingDate)
                .FirstOrDefault();
            return lastCashRegister;
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

