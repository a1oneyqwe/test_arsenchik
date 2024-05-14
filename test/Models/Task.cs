using System;

namespace test.Models
{
    public class Task
    {
        public int IdTask { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public int IdProject { get; set; }
        public int IdTaskType { get; set; }
        public int IdAssignedTo { get; set; }
        public int IdCreator { get; set; }

        // Navigation properties
        public Project Project { get; set; }
        public TeamMember Creator { get; set; }
        public TeamMember Assignee { get; set; }
        public TaskType TaskType { get; set; }
    }
}