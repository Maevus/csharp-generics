using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryIt
{
    public class EmployeeDb : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

    public interface IReadOnlyRepository<out T> : IDisposable // covariout outgoing variant.
    {
        T FindById(int id);
        IQueryable<T> FindAll();
    }

    public interface IWriteOnlyRepository<in T> : IDisposable //allowing covariance for incoming data. cannot apply to data with a return types.
    {
        void Add(T newEntity);
        void Delete(T entity);
        int Commit();
    }

    public interface IRepository<T> : IReadOnlyRepository<T>, IWriteOnlyRepository<T> // see irepository is still invariant, but gets covariance and contravariance through its interfaces.
    {

    }

    public class SqlRepository<T> : IRepository<T> where T : class, IEntity
                                                     //, new()
    {
        DbContext _ctx;
        DbSet<T> _set;

        public SqlRepository(DbContext ctx)
        {
            _ctx = ctx;
            _set = _ctx.Set<T>();
        }

        public void Add(T newEntity)
        {
            if (newEntity.IsValid())
            {
                _set.Add(newEntity);
            }
        }

        public void Delete(T entity)
        {
            _set.Remove(entity);
        }

        public T FindById(int id)
        {
            //T t = new T(); // note new() constraint ensures default constructor for T exists, therefor can initiate in this class
            return _set.Find(id);
        }

        public IQueryable<T> FindAll()
        {
            return _set;
        }

        public int Commit()
        {
            return _ctx.SaveChanges();
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}
