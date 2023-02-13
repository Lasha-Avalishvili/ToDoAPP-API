using ToDoApp_API.Db.Entities;
using ToDoApp_API.Models.Requests;

namespace ToDoApp_API.Repositories
{
    public interface ITodoRepository
    {
        Task InsertAsync(int userId, string title, string description, DateTime deadline);
        Task<IEnumerable<TodoEntity>> SearchAsync(int userId, string title);
        Task<bool> UpdateAsync(int id, UpdateTodoRequest request);
        Task SaveChangesAsync();
    }
}
