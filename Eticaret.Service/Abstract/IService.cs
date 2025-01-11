using Eticaret.Core.Entities;
using System.Linq.Expressions;

namespace Eticaret.Service.Abstract
{
    public interface IService<T> where T : class, IEntitiy, new()   
    {
        List<T> GetAll(Expression<Func<T , bool>> Expression);
        IQueryable<T> GetQueryable();
        T Get(Expression<Func<T , bool>> Expression);
        T Find(int Id);
        void Add(T entity); 
        void Update(T entity);  
        void Delete(T entity);
        int SaveChanges();
        //asenkron metotlar
      Task<T> FindAsync(int Id);
      Task<T> GetAsync(Expression<Func<T , bool>> Expression);
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> Expression);
        Task AddAsync(T entity);
       Task< int > SaveChangesAsync();
    }
}
