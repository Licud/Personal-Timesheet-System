using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Repositories;

namespace TimesheetSystem.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private ProjectRepository projectRepository;
        private TasksRepository tasksRepository;
        private TimeLogRepository timeLogRepository;

        public ProjectRepository ProjectRepository 
        {
            get 
            { 
                if(projectRepository == null)
                {
                    this.projectRepository = new ProjectRepository(context);
                }
                return this.projectRepository;
            }
        }

        public TasksRepository TasksRepository
        {
            get
            {
                if (tasksRepository == null)
                {
                    this.tasksRepository = new TasksRepository(context);
                }
                return this.tasksRepository;
            }
        }

        public TimeLogRepository TimeLogRepository
        {
            get
            {
                if (timeLogRepository == null)
                {
                    this.timeLogRepository = new TimeLogRepository(context);
                }
                return this.timeLogRepository;
            }
        }

        public ApplicationDbContext GetDbContext 
        {
            get 
            {
                if(this.context == null)
                {
                    context = new ApplicationDbContext();
                }
                return this.context;
            }
        }

        public void SaveChanges() 
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if(!this.disposed)
            {
                if(disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}