using ToDoApp_API.Db.Entities;

namespace ToDoApp_API.Repositories
{
    public interface ISendEmailRequestRepository
    {
        void Insert(SendEmailRequestEntity entity);
        Task SaveChangesAsync();
    }
}
