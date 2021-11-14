using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Detran.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; }

        TEntity Create(TEntity entity);

        public Task<TEntity> CreateAsync(TEntity entity);

        void Update(TEntity entity);

        IEnumerable<TEntity> GetAll();

        public TEntity GetById(int id);
        public Task<TEntity> GetByIdAsync(int id);

        IEnumerable<TEntity> GetWhere(Expression<Func<TEntity, bool>> queryLambda);

        TEntity Find(Expression<Func<TEntity, bool>> queryLambda);

        public Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> queryLambda);

        public async Task SaveChangesAsync() => await Context.SaveChangesAsync();
        public void SaveChanges() => Context.SaveChanges();

        void Delete(TEntity entity);
        void CreateTransaction();
        void Commit();
        void Rollback();
        void Save();
    }
}