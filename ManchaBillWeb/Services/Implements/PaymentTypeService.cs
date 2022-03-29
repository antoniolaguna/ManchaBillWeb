using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class PaymentTypeService : GenericService<PaymentType>, IPaymentTypeService
    {
    private readonly IPaymentTypeRepository paymentTypeRepository;

    public PaymentTypeService(IPaymentTypeRepository paymentTypeRepository) : base(paymentTypeRepository)
    {
        this.paymentTypeRepository = paymentTypeRepository;
    }

    public Task<List<PaymentType>> GetAllPaymentTypesActives()
    {
        return paymentTypeRepository.GetAllPaymentTypesActives();
    }

    public Task LogicDelete(int id)
    {
        return paymentTypeRepository.LogicDelete(id);
    }
}
}
