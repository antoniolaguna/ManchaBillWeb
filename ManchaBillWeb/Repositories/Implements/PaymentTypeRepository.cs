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
    public class PaymentTypeRepository : GenericRepository<PaymentType>, IPaymentTypeRepository
    {
        private readonly ApplicationDbContext context;
        public PaymentTypeRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<PaymentType>> GetAllPaymentTypesActives()
        {
            var listPaymentTypes = context.PaymentTypes
                .Where(x => x.Active)
                .ToListAsync();
            return listPaymentTypes;
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
