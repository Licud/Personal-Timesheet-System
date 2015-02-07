using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.App_Start
{
    public class UpdateDates
    {

        public static void UpdateDatabase()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            IEnumerable<Project> projects = unitOfWork.ProjectRepository.GetAllProjects();

            foreach(var project in projects)
            {
                project.TotalDuration = DateTime.Now.Subtract(project.ProjectDateStarted).Days;
                unitOfWork.ProjectRepository.UpdateProject(project);
                unitOfWork.SaveChanges();
            }        
        }
    }
}