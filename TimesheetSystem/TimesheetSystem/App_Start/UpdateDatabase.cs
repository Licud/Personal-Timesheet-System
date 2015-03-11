using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.App_Start
{
    public class UpdateDatabase
    {

        public static void UpdateDates()
        {
            UnitOfWork unitOfWork = new UnitOfWork();

            IEnumerable<Project> projects = unitOfWork.ProjectRepository.GetAllProjects();

            foreach(var project in projects.Where(p => p.ProjectStatus == "Ongoing"))
            {
                project.TotalDuration = DateTime.Now.Subtract(project.ProjectDateStarted).Days + 1;
                unitOfWork.ProjectRepository.UpdateProject(project);
                unitOfWork.SaveChanges();
            }
        }

    }
}