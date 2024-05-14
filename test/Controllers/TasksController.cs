using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using test.Models;
using test.Repositories;
using System;
using System.Linq;

namespace test.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        private readonly ILogger<TasksController> _logger;
        private readonly ITaskRepository _taskRepository;

        public TasksController(ILogger<TasksController> logger, ITaskRepository taskRepository)
        {
            _logger = logger;
            _taskRepository = taskRepository;
        }

        [HttpGet]
        public IActionResult GetTasks(int? projectId = null)
        {
            try
            {
                var tasks = _taskRepository.GetTasks(projectId)
                    .OrderBy(t => t.Deadline)
                    .Select(t => new
                    {
                        t.IdTask,
                        t.Name,
                        t.Description,
                        t.Deadline,
                        ProjectName = t.Project.Name,
                        CreatorLastName = t.Creator.LastName,
                        AssigneeLastName = t.Assignee.LastName,
                        TaskTypeName = t.TaskType.Name
                    })
                    .ToList();

                return Ok(tasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving tasks");
                return StatusCode(500, "An error occurred while retrieving tasks");
            }
        }

        [HttpPost]
        public IActionResult AddTask([FromBody] TaskDto taskDto)
        {
            if (taskDto == null)
            {
                return BadRequest("Invalid request body");
            }

            try
            {
                int taskId = _taskRepository.AddTask(taskDto);
                return Ok(taskId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a task");
                return StatusCode(500, "An error occurred while adding a task");
            }
        }
    }
}
