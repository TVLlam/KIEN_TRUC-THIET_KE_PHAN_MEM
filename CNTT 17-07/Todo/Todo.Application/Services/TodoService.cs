using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Todo.Domain.Repositories;
using Todo.Infrastructure.Repositories;

namespace Todo.Application.Services
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _repository;
        public TodoService(ITodoRepository repository)
        {
            _repository = repository;
        }
        public async Task Add(Infrastructure.Todo entity)
        {
            await _repository.AddAsync(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            _repository.Delete(entity);
            await _repository.SaveChangesAsync();
        }

        public async Task<List<Infrastructure.Todo>> GetAll()
        {
            var todos = await _repository.GetAllAsync();

            return todos.ToList();
        }

        public async Task<Infrastructure.Todo> GetById(int id)
        {
            var todo = await _repository.GetByIdAsync(id);
            return todo;
        }

        public async Task Update(Infrastructure.Todo entity)
        {
            var todo = await _repository.GetByIdAsync(entity.Id);
            if (todo == null)
            {
                todo.Title = entity.Title;
                todo.IsCompleted = entity.IsCompleted;
                _repository.Update(todo);
                await _repository.SaveChangesAsync();
            }
        }
    }
}
