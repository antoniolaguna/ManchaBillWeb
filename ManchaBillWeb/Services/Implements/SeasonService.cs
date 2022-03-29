using ManchaBillWeb.Repositories;
using ManchaBillWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class SeasonService : GenericService<Season>, ISeasonService
    {
        private readonly ISeasonRepository seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository) : base(seasonRepository)
        {
            this.seasonRepository = seasonRepository;
        }

        public Task<List<Season>> GetAllSeasonsActives()
        {
            return seasonRepository.GetAllSeasonsActives();
        }

        public Task LogicDelete(int id)
        {
            return seasonRepository.LogicDelete(id);
        }
    }
}
