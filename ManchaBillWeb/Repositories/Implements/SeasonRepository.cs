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
    public class SeasonRepository : GenericRepository<Season>, ISeasonRepository
    {
        private readonly ApplicationDbContext context;
        public SeasonRepository(ApplicationDbContext context) : base(context)
        {
            this.context = context;
        }

        public Task<List<Season>> GetAllSeasonsActives()
        {
            var listSeaqsons = context.Seasons
                .Where(x => x.Active)
                .ToListAsync();
            return listSeaqsons;
        }

        public async Task LogicDelete(int id)
        {
            var entity = await GetById(id);
            entity.DeleteDate = DateTime.Now;
            entity.Active = false;
            context.Update(entity);
            await context.SaveChangesAsync();
        }


    }
}


