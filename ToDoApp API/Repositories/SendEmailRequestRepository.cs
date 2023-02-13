using ToDoApp_API.Db.Entities;
using ToDoApp_API.Db;

namespace ToDoApp_API.Repositories
{
    public class SendEmailRequestRepository : ISendEmailRequestRepository
    {
        private readonly AppDbContext _db;

        public SendEmailRequestRepository(AppDbContext db)
        {
            _db = db;
        }

        public void Insert(SendEmailRequestEntity entity)
        {
            _db.SendEmailRequests.Add(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
