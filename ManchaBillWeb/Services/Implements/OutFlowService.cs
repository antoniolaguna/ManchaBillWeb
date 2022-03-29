using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ManchaBillWeb.Services.Implements
{
    public class OutFlowService : GenericService<OutFlow>, IOutFlowService
    {
        private readonly IOutFlowRepository outFlowRepository;

        public OutFlowService(IOutFlowRepository seasonRepository) : base(seasonRepository)
        {
            this.outFlowRepository = seasonRepository;
        }

        public Task<List<OutFlow>> GetAllOutFlowsActives()
        {
            return outFlowRepository.GetAllOutFlowsActives();
        }

        public Task LogicDelete(int id)
        {
            return outFlowRepository.LogicDelete(id);
        }
    }
}
