using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManchaBillWeb.Services
{
    public interface IGenericService<TEntity> where TEntity : class
    {
        Task<List<TEntity>> GetAll();

        Task<TEntity> GetById(int id);

        TEntity Insert(TEntity entity);

        TEntity Update(TEntity entity);

        Task Delete(int id);

        Task<Boolean> Exists(int id);
    }
}
