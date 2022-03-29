using ManchaBillWeb.Models;
using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class ParameterService : GenericService<Parameter>, IParameterService
    {
        private readonly IParameterRepository parameterRepository;
        public ParameterService(IParameterRepository parameterRepository) : base(parameterRepository)
        {
            this.parameterRepository = parameterRepository;
        }

        public Task<List<Parameter>> GetAllParametersActives()
        {
            return this.parameterRepository.GetAllParametersActives();
        }

        public Task LogicDelete(int id)
        {
            return this.parameterRepository.LogicDelete(id);
        }
        public Parameter GetParameterByName(string name)
        {
            return this.parameterRepository.GetParameterByName(name);
        }
    }
}
