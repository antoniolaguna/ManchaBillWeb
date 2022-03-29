using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class SupplierService : GenericService<Supplier>, ISupplierService
    {
        private readonly ISupplierRepository supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository) : base(supplierRepository)
        {
            this.supplierRepository = supplierRepository;
        }

        public Task<List<Supplier>> GetAllSuppliersActives()
        {
            return supplierRepository.GetAllSuppliersActives();
        }

        public Task LogicDelete(int id)
        {
            return supplierRepository.LogicDelete(id);
        }
    }
}
