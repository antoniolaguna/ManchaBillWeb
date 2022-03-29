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
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly ApplicationDbContext context;

        public GenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
                throw new Exception("The entity is null");

            context.Set<TEntity>().Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetById(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public TEntity Insert(TEntity entity)
        {
            try
            {
                context.Set<TEntity>().Add(entity);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());

            }

            return entity;
        }

        public  TEntity Update(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            context.SaveChanges();
            return entity;
        }

        public async Task<Boolean> Exists(int id)
        {
            var entity = await GetById(id);

            if (entity == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
