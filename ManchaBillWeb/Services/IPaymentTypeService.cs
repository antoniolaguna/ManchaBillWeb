using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IPaymentTypeService : IGenericService<PaymentType>
    {
        Task<List<PaymentType>> GetAllPaymentTypesActives();
        Task LogicDelete(int id);
    }
}
