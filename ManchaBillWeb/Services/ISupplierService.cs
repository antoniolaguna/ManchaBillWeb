using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services
{
    public interface ISupplierService : IGenericService<Supplier>
    {
        Task<List<Supplier>> GetAllSuppliersActives();
        Task LogicDelete(int id);
    }
}