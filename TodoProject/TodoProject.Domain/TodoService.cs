using TodoProject.Domain.Entities;
using TodoProject.Domain.Interfaces.Repositories;
using TodoProject.Domain.Interfaces.Services;

namespace TodoProject.Domain
{
    public class TodoService : ITodoService
    {
        private readonly ITodoRepository _todoRepository;

        public TodoService( ITodoRepository todoRepository )
        {
            _todoRepository = todoRepository;
        }

        public async Task<TodoItem> GetTodoByIdAsync( Guid id )
        {
            return await _todoRepository.GetByIdAsync( id );
        }

        public async Task<IEnumerable<TodoItem>> GetAllTodosAsync()
        {
            return await _todoRepository.GetAllAsync();
        }

        public async Task CreateTodoAsync( TodoItem item )
        {
            await _todoRepository.AddAsync( item );
        }

        public async Task UpdateTodoAsync( TodoItem item )
        {
            await _todoRepository.UpdateAsync( item );
        }

        public async Task DeleteTodoAsync( Guid id )
        {
            var item = await _todoRepository.GetByIdAsync( id );
            if( item != null )
            {
                await _todoRepository.DeleteAsync( item );
            }
        }
    }
}
