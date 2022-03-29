using ManchaBillWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services.Implements
{
    public class GenericService<TEntity> : IGenericService<TEntity> where TEntity : class
    {
        private readonly IGenericRepository<TEntity> genericRepository;

        public GenericService(IGenericRepository<TEntity> genericRepository)
        {
            this.genericRepository = genericRepository;
        }

        public async Task Delete(int id)
        {
            await genericRepository.Delete(id);
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await genericRepository.GetAll();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await genericRepository.GetById(id);
        }

        public TEntity Insert(TEntity entity)
        {
            return genericRepository.Insert(entity);
        }

        public TEntity Update(TEntity entity)
        {
            return genericRepository.Update(entity);
        }

        public async Task<Boolean> Exists(int id)
        {
            return await genericRepository.Exists(id);
        }
    }
}
