using Moq;
using TodoProject.Domain;
using TodoProject.Domain.Entities;
using TodoProject.Domain.Interfaces.Repositories;

namespace TodoProject.Test
{
    [TestClass]
    public class TodoTest
    {
        [TestMethod]
        public void AddTodoAsync_ShouldInvokeAddOnRepository_WhenCalled()
        {
            // Arrange
            var mockRepository = new Mock<ITodoRepository> ();
            var todoService = new TodoService( mockRepository.Object );
            var newTodo = new TodoItem { Name = "New Todo", IsComplete = false };

             mockRepository.Setup( repo => repo.AddAsync( It.IsAny<TodoItem>() ) )
                  .Verifiable( "Repository method AddAsync was not called with the expected parameters." );

            // Act
            todoService.CreateTodoAsync( newTodo ).Wait();

            // Assert
            mockRepository.Verify( repo => repo.AddAsync( It.Is<TodoItem>( t => t.Name == "New Todo" && t.IsComplete == false ) ), Times.Once() );
        }
    }
}