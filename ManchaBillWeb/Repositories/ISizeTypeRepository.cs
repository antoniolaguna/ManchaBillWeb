using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories
{
    public  interface ISizeTypeRepository:IGenericRepository<SizeType>
    {
        Task<List<SizeType>> GetAllSizeTypesActives();
        Task LogicDelete(int id);
    }
}
