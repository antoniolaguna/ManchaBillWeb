using ManchaBillWeb.Data;
using ManchaBillWeb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Repositories.Implements
{
    public class ParameterRepository : GenericRepository<Parameter>, IParameterRepository
    {
        private readonly ApplicationDbContext context;
        public ParameterRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Parameter>> GetAllParametersActives()
        {
            var result = this.context.Parameters.Where(parameter => parameter.Active)
                .ToListAsync();

            return result;
        }

        public Parameter GetParameterByName(string name)
        {
            var result = this.context.Parameters.Where(parameter => parameter.Active && parameter.Name.Equals(name))
                .ToList();

            return result[0];
        }

        public Task LogicDelete(int id)
        {
            Parameter parameterToLogicDelete = context.Parameters.Find(id);
            parameterToLogicDelete.DeleteDate = DateTime.Now;
            parameterToLogicDelete.Active = false;
            context.Update(parameterToLogicDelete);
            return context.SaveChangesAsync();
        }

       
    }
}
