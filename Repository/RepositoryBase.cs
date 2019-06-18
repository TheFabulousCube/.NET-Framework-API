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

        //Do not expose LINQ methods
        //Let’s get it straight.There are no complete LINQ to SQL implementations.They all are either missing features or implement things like eager/lazy loading in their own way.That means that they all are leaky abstractions. So if you expose LINQ outside your repository you get a leaky abstraction.You could really stop using the repository pattern then and use the OR/M directly.
        // 
        //public interface IRepository<TEntity>
        //    {
        //        IQueryable<TEntity> Query();

        //        // [...]
        //    }
        //Those repositories really do not serve any purpose.They are just lipstick on a pig. Use your ORM directly instead.
        /***************************
         * Instead, return the type and use .FirstOrDefault or .ToList<TEntity>() to complete the query
         * Entity Framework, ToList vs AsEnumerable vs AsQueryable
                ToList()
                Execute the query immediately

                AsEnumerable()
                lazy (execute the query later)
                Parameter: Func&lt;TSource, bool&gt;
                Load EVERY record into application memory, and then handle/filter them. (e.g. Where/Take/Skip, it will select * from table1, into the memory, then select the first X elements) (In this case, what it did: Linq-to-SQL + Linq-to-Object)

                AsQueryable()
                lazy (execute the query later)
                Parameter: Expression&lt;Func&lt;TSource, bool&gt;&gt;
                Convert Expression into T-SQL (with the specific provider), query remotely and load result to your application memory.
                That’s why DbSet (in Entity Framework) also inherits IQueryable to get the efficient query.
                Do not load every record, e.g. if Take(5), it will generate select top 5 * SQL in the background. This means this type is more friendly to SQL Database, and that is why this type usually has higher performance and is recommended when dealing with a database.

                So AsQueryable() usually works much faster than AsEnumerable() as it generate T-SQL at first, which includes all your where conditions in your Linq.
         **********************************************/

        public IEnumerable<T> FindAll()
        {
            return this.RepositoryContext.Set<T>().AsNoTracking().ToList<T>();
            /***
             * This is FindAll(), with no filtering or predicate, IEnumerable() is fine, it will lazy load
             * For testing, I want to see the actual list during debug in the controller.
            ***/
        }

        //Invoke ToList() before returning
        //The query is not executed in the database until you invoke ToList(), FirstOrDefault() etc.So if you want to be able to keep all data related exceptions in the repositories you have to invoke those methods.

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.RepositoryContext.Set<T>().Where(expression).AsNoTracking();
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
            this.RepositoryContext.Set<T>().Remove(entity);
        }
    }
}
