using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Domain.Repositories
{
    public interface IRepositoty<T> where T : class
    {   
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task SaveChangesAsync();
        void UpdateAsync(T entity);
        void DeleteAsync(T entity);
    }
}
