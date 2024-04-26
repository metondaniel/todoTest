using Microsoft.AspNetCore.Mvc;
using TodoProject.Domain;
using TodoProject.Domain.Entities;
using TodoProject.Domain.Interfaces.Services;

namespace TodoProject.Controllers
{
    [Route( "api/[controller]" )]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly ITodoService _todoService;

        public TodoController( ITodoService todoService )
        {
            _todoService = todoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            try
            {
                return Ok( await _todoService.GetAllTodosAsync() );
            }
            catch ( Exception ex )
            {
                return BadRequest( ex );
            }
        }

        [HttpGet( "{id}" )]
        public async Task<ActionResult<TodoItem>> GetTodoItem( Guid id )
        {
            try
            {
                var todoItem = await _todoService.GetTodoByIdAsync( id );

                if( todoItem == null )
                {
                    return NotFound();
                }

                return todoItem;
            }
            catch( Exception ex )
            {
                return BadRequest( ex );
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem( TodoItem item )
        {
            try
            {
                await _todoService.CreateTodoAsync( item );

                return CreatedAtAction( nameof( GetTodoItem ), new { id = item.Id }, item );
            }
            catch( Exception ex )
            {
                return BadRequest( ex );
            }
        }

        [HttpPut( "{id}" )]
        public async Task<IActionResult> PutTodoItem( Guid id, TodoItem item )
        {
            try
            {
                if( id != item.Id )
                {
                    return BadRequest();
                }

                await _todoService.UpdateTodoAsync( item );

                return NoContent();
            }
            catch ( Exception ex )
            {
                return BadRequest( ex );
            }
        }

        [HttpDelete( "{id}" )]
        public async Task<IActionResult> DeleteTodoItem( Guid id )
        {
            try
            {
                var todoItem = await _todoService.GetTodoByIdAsync( id );

                if( todoItem == null )
                {
                    return NotFound();
                }

                await _todoService.DeleteTodoAsync( id );

                return NoContent();
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
