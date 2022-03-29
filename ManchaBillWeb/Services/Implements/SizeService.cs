using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class SizeService : GenericService<Size>, ISizeService
    {
        private readonly ISizeRepository sizeRepository;
        public SizeService(ISizeRepository sizeRepository) : base(sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }

        public Task<List<Size>> GetAllSizesActives()
        {
            return this.sizeRepository.GetAllSizesActives();
        }

        public Task LogicDelete(int id)
        {
            return this.sizeRepository.LogicDelete(id);
        }

        public Task<List<Size>> GetSizesBySizeType(int sizeTypeID)
        {
            return this.sizeRepository.GetSizesBySizeType(sizeTypeID);
        }
    }
}
