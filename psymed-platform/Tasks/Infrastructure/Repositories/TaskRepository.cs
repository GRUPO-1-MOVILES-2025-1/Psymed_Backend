using Microsoft.EntityFrameworkCore;
using psymed_platform.Shared.Infrastructure.Persistence.EFC.Configuration;
using psymed_platform.Tasks.Domain.Model.Repositories;

namespace psymed_platform.Tasks.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;

        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Domain.Model.Aggregates.Task>> GetAllAsync()
        {
            return await _context.Set<Domain.Model.Aggregates.Task>().ToListAsync();
        }

        public async Task<Domain.Model.Aggregates.Task> GetByIdAsync(string id)
        {
            return await _context.Set<Domain.Model.Aggregates.Task>()
                .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Domain.Model.Aggregates.Task> AddAsync(Domain.Model.Aggregates.Task task)
        {
            _context.Set<Domain.Model.Aggregates.Task>().Add(task);
            await _context.SaveChangesAsync(); // 🔑 CLAVE: Guardar en BD
            return task;
        }

        public async Task<Domain.Model.Aggregates.Task> UpdateAsync(Domain.Model.Aggregates.Task task)
        {
            _context.Set<Domain.Model.Aggregates.Task>().Update(task);
            await _context.SaveChangesAsync(); // 🔑 CLAVE: Guardar cambios
            return task;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var task = await GetByIdAsync(id);
            if (task == null) return false;
            
            _context.Set<Domain.Model.Aggregates.Task>().Remove(task);
            await _context.SaveChangesAsync(); // 🔑 CLAVE: Guardar eliminación
            return true;
        }
    }
}