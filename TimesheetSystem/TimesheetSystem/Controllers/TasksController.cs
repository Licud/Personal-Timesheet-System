using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TimesheetSystem.DAL;
using TimesheetSystem.DAL.Models;
using TimesheetSystem.ViewModels;

namespace TimesheetSystem.Controllers
{
    [Authorize]
    public class TasksController : Controller
    {
        private UnitOfWork repositories = new UnitOfWork();

        
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectTasksViewModel projectTasks = new ProjectTasksViewModel();
            projectTasks.Project = repositories.ProjectRepository.GetProject(id.Value);
            projectTasks.Tasks = repositories.TasksRepository.GetAllTasksUnderProject(id.Value);

            return View(projectTasks);
        }

        public ActionResult AllTasks()
        {
            return View(repositories.TasksRepository.GetAllTasks());
        }

        // GET: Tasks/Create
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View(new Tasks() { ProjectId = id.Value });
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TasksName,TaskType,TaskStartDate,EstimatedTaskDateEnd,TasksStatus,ProjectId")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.TasksStatus = "Ongoing";
                tasks.TaskDuration = tasks.EstimatedTaskDateEnd.Subtract(tasks.TaskStartDate).Days + 1;
                repositories.TasksRepository.CreateTask(tasks);
                repositories.SaveChanges();
                return RedirectToAction("Index", new { id = tasks.ProjectId } );
            }
        
            return View(tasks);
        }

        // GET: Tasks/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = repositories.TasksRepository.GetTask(id.Value);
            if (tasks == null)
            {
                return HttpNotFound();
            }

            return View(tasks);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TasksId,TasksName,TaskType,TaskStartDate,EstimatedTaskDateEnd,TaskDuration,TasksStatus,ProjectId")] Tasks tasks)
        {
            if (ModelState.IsValid)
            {
                tasks.TaskDuration = tasks.EstimatedTaskDateEnd.Subtract(tasks.TaskStartDate).Days + 1;
                repositories.TasksRepository.UpdateTask(tasks);
                repositories.SaveChanges();
                return RedirectToAction("Index", new { id = tasks.ProjectId });
            }
            ViewBag.ProjectId = new SelectList(repositories.ProjectRepository.GetAllProjects() , "ProjectId", "ProjectName", tasks.ProjectId);

            return View(tasks);
        }

        // GET: Tasks/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tasks tasks = repositories.TasksRepository.GetTask(id.Value);
            if (tasks == null)
            {
                return HttpNotFound();
            }
            return View(tasks);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int projectId = repositories.TasksRepository.GetTask(id).ProjectId;

            repositories.TasksRepository.RemoveTask(id);
            repositories.SaveChanges();

            return RedirectToAction("Index", new { id = projectId });
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id)
        {
            Tasks task = repositories.TasksRepository.GetTask(id);
            if (task.TasksStatus == "Ongoing")
            {
                task.TasksStatus = "Finished";
            }
            else
            {
                task.TasksStatus = "Ongoing";
            }

            repositories.SaveChanges();
            var result = new { identifier = task.TasksId, newStatus = task.TasksStatus };
            
            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repositories.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
