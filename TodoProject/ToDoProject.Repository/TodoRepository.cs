using MongoDB.Driver;
using TodoProject.Domain.Entities;
using TodoProject.Domain.Interfaces.Repositories;

namespace ToDoProject.Repository
{
    public class TodoRepository : ITodoRepository
    {
        private readonly IMongoCollection<TodoItem> _todos;

        public TodoRepository( MongoSettings settings )
        {
            var client = new MongoClient( settings.ConnectionString );
            var database = client.GetDatabase( settings.DatabaseName );
            _todos = database.GetCollection<TodoItem>( "Todos" );
        }

        public async Task AddAsync( TodoItem item )
        {
            await _todos.InsertOneAsync( item );
        }

        public async Task DeleteAsync( TodoItem item )
        {
            await _todos.DeleteOneAsync( x => x.Id == item.Id );
        }

        public async Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return await _todos.Find( _ => true ).ToListAsync();
        }

        public async Task<TodoItem> GetByIdAsync( Guid id )
        {
            return await _todos.Find( x => x.Id == id ).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync( TodoItem item )
        {
            await _todos.ReplaceOneAsync( x => x.Id == item.Id, item );
        }
    }
}
