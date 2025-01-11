using Eticaret.Core.Entities;
using Eticaret.Data;
using Eticaret.Service.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Eticaret.Service.Concrete
{
    public class Service<T> : IService<T> where T : class, IEntitiy,new()
    {
        internal DatabaseContext _context;
        internal DbSet<T> _dbSet;

        public Service(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public async Task AddAsync(T entity)
        {
           await _dbSet.AddAsync(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public T Find(int Id)
        {
            return  _dbSet.Find(Id);
        }

        public async Task<T> FindAsync(int Id)
        {
              return await _dbSet.FindAsync(Id);
        }

        public T Get(Expression<Func<T, bool>> Expression)
        {
            return _dbSet.FirstOrDefault(Expression);
        }

        public List<T> GetAll()
        {
           return _dbSet.AsNoTracking().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> Expression)
        {
            return _dbSet.AsNoTracking().ToList();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> Expression)
        {
            return await _dbSet.Where(Expression).AsNoTracking().ToListAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> Expression)
        {
            return await _dbSet.FirstOrDefaultAsync(Expression);
        }

        public IQueryable<T> GetQueryable()
        {
          return _dbSet;
        }

        public  int SaveChanges()
        {
            return  _context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    }
}
