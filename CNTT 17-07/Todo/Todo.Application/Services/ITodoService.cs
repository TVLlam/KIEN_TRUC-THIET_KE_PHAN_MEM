using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.Application.Services
{
    public interface ITodoService
    {
        Task<List<Todo.Infrastructure.Todo>> GetAll();
        Task<Todo.Infrastructure.Todo> GetById(int id);
        Task Update(Todo.Infrastructure.Todo entity);
        Task Add(Todo.Infrastructure.Todo entity);
        Task Delete(int id);
    }
}
