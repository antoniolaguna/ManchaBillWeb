using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class SizeTypeService: GenericService<SizeType>, ISizeTypeService
    {
        private readonly ISizeTypeRepository sizeTypeRepository;

        public SizeTypeService(ISizeTypeRepository sizeTypeRepository) : base(sizeTypeRepository)
        {
            this.sizeTypeRepository = sizeTypeRepository;
        }

        public Task<List<SizeType>> GetAllSizeTypesActives()
        {
            return sizeTypeRepository.GetAllSizeTypesActives();
        }

        public Task LogicDelete(int id)
        {
            return sizeTypeRepository.LogicDelete(id);
        }
    }
}
