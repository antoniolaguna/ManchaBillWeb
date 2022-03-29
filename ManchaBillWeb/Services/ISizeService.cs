using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface ISizeService:IGenericService<Size>
    {
        Task<List<Size>> GetAllSizesActives();
        Task LogicDelete(int id);
        Task<List<Size>> GetSizesBySizeType(int sizeTypeID);
    }
}
