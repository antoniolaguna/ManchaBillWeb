using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class ReturnService : GenericService<Return>, IReturnService
    {
        private readonly IReturnRepository returnRepository;
        public ReturnService(IReturnRepository returnRepository) : base(returnRepository)
        {
            this.returnRepository = returnRepository;
        }

        public Task<List<Return>> GetAllReturnsActives()
        {
            return this.returnRepository.GetAllReturnsActives();
        }

        public Task LogicDelete(int id)
        {
            return this.returnRepository.LogicDelete(id);
        }

        public Return GetByIdWithAllRelationships(int id)
        {
            return this.returnRepository.GetByIdWithAllRelationships(id);
        }
    }
}
