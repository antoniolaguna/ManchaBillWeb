using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public interface ISupplierRepository : IGenericRepository<Supplier>
    {
        Task<List<Supplier>> GetAllSuppliersActives();
        Task LogicDelete(int id);
    }
}

