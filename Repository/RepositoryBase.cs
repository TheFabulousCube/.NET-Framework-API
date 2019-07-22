using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected RepositoryContext RepositoryContext { get; set; }

        public RepositoryBase(RepositoryContext repositoryContext)
        {
            this.RepositoryContext = repositoryContext;
        }

        public IEnumerable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking().ToList<T>();
            /***
             * This is FindAll(), with no filtering or predicate, IEnumerable() is fine, it will lazy load
             * For testing, I want to see the actual list during debug in the controller.
            ***/
        }

        public IEnumerable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<T> FindForRemoval(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>()
                .Where(expression)
                .ToList();
        }

        public void RemoveByCondition(Expression<Func<T, bool>> expression)
        {
            this.RepositoryContext.Set<T>().RemoveRange(FindForRemoval(expression));
        }

        public void Create(T entity)
        {
            this.RepositoryContext.Set<T>().Add(entity);
        }

        public void Update(T entity)
        {
            this.RepositoryContext.Set<T>().AddOrUpdate(entity);
        }

        public void Delete(T entity)
        {
            this.RepositoryContext.Set<T>().Attach(entity);
            this.RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
