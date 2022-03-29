using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class ReturnLineService : GenericService<ReturnLine>, IReturnLineService
    {
        private readonly IReturnLineRepository returnLineRepository;
        public ReturnLineService(IReturnLineRepository returnLineRepository) : base(returnLineRepository)
        {
            this.returnLineRepository = returnLineRepository;
        }

        public Task<List<ReturnLine>> GetAllReturnLinesActives()
        {
            return this.returnLineRepository.GetAllReturnLinesActives();
        }
        public Task<List<ReturnLine>> GetAllReturnLinesActivesByReturn(int idReturn)
        {
            return this.returnLineRepository.GetAllReturnLinesActivesByReturn(idReturn);
        }

        public Task LogicDelete(int id)
        {
            return this.returnLineRepository.LogicDelete(id);
        }

        public ReturnLine GetByIdWithAllRelationships(int id)
        {
            return this.returnLineRepository.GetByIdWithAllRelationships(id);
        }
    }
}
