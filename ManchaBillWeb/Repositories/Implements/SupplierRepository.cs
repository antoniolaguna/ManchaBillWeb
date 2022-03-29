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
    public class SupplierRepository : GenericRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext context;
        public SupplierRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Supplier>> GetAllSuppliersActives()
        {
            var listSupplier = context.Suppliers
                .Where(x => x.Active)
                .ToListAsync();
            return listSupplier;
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
