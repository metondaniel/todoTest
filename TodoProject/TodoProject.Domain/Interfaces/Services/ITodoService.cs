using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoProject.Domain.Entities;

namespace TodoProject.Domain.Interfaces.Services
{
    public interface ITodoService
    {
        Task<TodoItem> GetTodoByIdAsync( Guid id );
        Task<IEnumerable<TodoItem>> GetAllTodosAsync();
        Task CreateTodoAsync( TodoItem item );
        Task UpdateTodoAsync( TodoItem item );
        Task DeleteTodoAsync( Guid id );
    }
}
