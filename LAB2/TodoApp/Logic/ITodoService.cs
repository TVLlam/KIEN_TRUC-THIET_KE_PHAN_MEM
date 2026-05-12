using System.Collections.Generic;
using System.Threading.Tasks;

namespace TodoApp
{
    public interface ITodoService
    {
        public Task<List<Todo>> GetAllAsync();
        public Task<Todo> GetAsync(int id);
        public Task AddAsync(string title);
        public Task DeleteAsync(int id);
        public Task UpdateAsync(int id, string title);
        public Task ToggleTodoAsync(int id);
    }
}
