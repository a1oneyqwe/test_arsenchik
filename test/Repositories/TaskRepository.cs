using System.Linq;
using test.Models;

namespace test.Repositories
{
    public interface ITaskRepository
    {
        IQueryable<Task> GetTasks(int? projectId = null);
        int AddTask(TaskDto taskDto);
    }

    public class TaskRepository : ITaskRepository
    {
        private readonly YourDbContext _context;

        public TaskRepository(YourDbContext context)
        {
            _context = context;
        }

        public IQueryable<Task> GetTasks(int? projectId = null)
        {
            IQueryable<Task> query = _context.Tasks.AsQueryable();

            if (projectId.HasValue)
            {
                query = query.Where(t => t.IdProject == projectId);
            }

            return query;
        }

        public int AddTask(TaskDto taskDto)
        {
            var task = new Task
            {
                Name = taskDto.Name,
                Description = taskDto.Description,
                Deadline = taskDto.Deadline,
                IdProject = taskDto.IdProject,
                IdTaskType = taskDto.IdTaskType,
                IdAssignedTo = taskDto.IdAssignedTo,
                IdCreator = taskDto.IdCreator
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return task.IdTask;
        }
    }
}