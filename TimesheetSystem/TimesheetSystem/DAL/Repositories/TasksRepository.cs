using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.DAL.Repositories
{
    public class TasksRepository
    {
        private ApplicationDbContext context;

        public TasksRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<Tasks> GetAllTasks()
        {
            return context.Tasks.ToList();
        }

        public IEnumerable<Tasks> GetAllTasksUnderProject(int id)
        {
            return context.Tasks.Where(p => p.ProjectId == id).ToList();
        }

        public Tasks GetTask(int id)
        {
            return context.Tasks.Find(id);
        }

        public void CreateTask(Tasks _tasks)
        {
            context.Tasks.Add(_tasks);
        }

        public void EditTask(Tasks _tasks)
        {
            context.Entry(_tasks).State = EntityState.Modified;
        }

        public void RemoveTask(int id)
        {
            Tasks taskToBeRemoved = context.Tasks.Find(id);
            context.Tasks.Remove(taskToBeRemoved);
        }
    }
}