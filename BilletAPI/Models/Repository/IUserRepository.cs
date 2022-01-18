using System;
using System.Collections.Generic;

namespace BilletAPI.Models.Repository
{
    public interface IUserRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();
        TEntity Get(long id);
        void Add(TEntity entity);
        void Login(string email, string password);
        void Update(TEntity entityToUpdate, TEntity entity);
        void Delete(long id);
    }
}
