using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Detran.Infrastructure.Context;

namespace Detran.Infrastructure.Repository
{
    public class Repository<TEntity> : IDisposable, IRepository<TEntity> where TEntity : class
    {
        private MySqlContext _context;
        private IDbContextTransaction _transaction;

        public Repository(MySqlContext dbContext)
        {
            _context = dbContext;
        }

        public DbContext Context
        {
            get { return this._context; }
        }

        public IQueryable<TEntity> GetAll()
        {
            return _context.Set<TEntity>();
        }

        //INSERIR
        TEntity IRepository<TEntity>.Create(TEntity entity)
        {
            _context.Set<TEntity>().Add(entity);
            return entity;
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            return result.Entity;
        }

        //ATUALIZAR
        void IRepository<TEntity>.Update(TEntity entity)
        {
            var result = _context.Set<TEntity>().FirstOrDefault(p => p.Equals(entity));
            if (result != null) _context.Entry(result).CurrentValues.SetValues(entity);
        }

        //BUSCAR COM CLÁUSULA
        IEnumerable<TEntity> IRepository<TEntity>.GetWhere(Expression<Func<TEntity, bool>> queryLambda)
        {
            return _context.Set<TEntity>().Where(queryLambda).AsEnumerable();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> queryLambda)
        {
            return _context.Set<TEntity>().Where(queryLambda).FirstOrDefault();
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> queryLambda)
        {
            return await _context.Set<TEntity>().Where(queryLambda).FirstOrDefaultAsync();
        }

        //REMOVER
        void IRepository<TEntity>.Delete(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        //BUSCAR TODOS
        IEnumerable<TEntity> IRepository<TEntity>.GetAll()
        {
            return _context.Set<TEntity>().AsEnumerable();
        }

        //BUSCAR POR ID
        TEntity IRepository<TEntity>.GetById(int id)
        {
            return _context.Set<TEntity>().Find(id);
        }
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FindAsync(id);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual async Task SaveChangesAsync()
        {
            await Context.SaveChangesAsync();
        }

        public virtual void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void CreateTransaction()
        {
            if (Context != null)
            {
                _transaction = Context.Database.BeginTransaction();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

        public void Rollback()
        {
            if (Context != null)
            {
                _transaction.Rollback();
                _transaction.Dispose();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }

        public void Commit()
        {
            if (Context != null)
            {
                _transaction.Commit();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }
        public void Save()
        {
            if (Context != null)
            {
                Context.SaveChanges();
            }
            else
            {
                throw new ArgumentNullException("Context UnitOfWork é null");
            }
        }
    }
}