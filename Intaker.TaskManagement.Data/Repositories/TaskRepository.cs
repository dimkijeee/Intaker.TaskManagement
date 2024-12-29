using Intaker.TaskManagement.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Intaker.TaskManagement.Data.Repositories
{
    public class TaskRepository : IRepository<Models.Task>
    {
        private readonly ApplicationContext _context;

        public TaskRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Models.Task> Get(int id)
        {
            return await _context.Tasks.FirstOrDefaultAsync(task => task.Id == id);
        }

        public async Task<IEnumerable<Models.Task>> GetAll()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task Create(Models.Task entity)
        {
            await _context.Tasks.AddAsync(entity);
        }

        public Task Update(Models.Task entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return Task.CompletedTask;
        }

        public Task Delete(Models.Task entity)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
