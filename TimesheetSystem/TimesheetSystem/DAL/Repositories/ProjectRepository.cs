using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.DAL.Repositories
{
    public class ProjectRepository
    {
        private ApplicationDbContext context;

        public ProjectRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<Project> GetAllProjects()
        {
            return context.Project.ToList();
        }

        public Project GetProject(int id)
        {
            return context.Project.Find(id);
        }

        public void CreateProject(Project _project)
        {
            context.Project.Add(_project);
        }

        public void UpdateProject(Project _project)
        {
            context.Entry(_project).State = EntityState.Modified;
        }

        public void RemoveProject(int id)
        {
            Project projectToBeRemoved = context.Project.Find(id);
            context.Project.Remove(projectToBeRemoved);
        }
    }
}