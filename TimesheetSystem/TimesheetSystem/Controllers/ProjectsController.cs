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

namespace TimesheetSystem.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private UnitOfWork repositories = new UnitOfWork();

        // GET: Projects
        public ActionResult Index()
        {
            return View(repositories.ProjectRepository.GetAllProjects());
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProjectName,ProjectType,ProjectDateStarted,EstimatedProjectDateEnd")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.EstimatedDuration = project.EstimatedProjectDateEnd.Subtract(project.ProjectDateStarted).Days + 1;
                project.TotalDuration = DateTime.Now.Subtract(project.ProjectDateStarted).Days + 1;
                project.ProjectStatus = "Ongoing";

                repositories.ProjectRepository.CreateProject(project);
                repositories.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = repositories.ProjectRepository.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProjectId,ProjectName,ProjectType,ProjectDateStarted,EstimatedProjectDateEnd,EstimatedDuration,TotalDuration,ProjectStatus")] Project project)
        {
            if (ModelState.IsValid)
            {
                project.EstimatedDuration = project.EstimatedProjectDateEnd.Subtract(project.ProjectDateStarted).Days + 1;
                project.TotalDuration = DateTime.Now.Subtract(project.ProjectDateStarted).Days + 1;
                repositories.ProjectRepository.UpdateProject(project);
                repositories.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = repositories.ProjectRepository.GetProject(id.Value);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            repositories.ProjectRepository.RemoveProject(id);
            repositories.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult UpdateStatus(int id)
        {
            Project project = repositories.ProjectRepository.GetProject(id);
            if (project.ProjectStatus == "Ongoing")
            {
                project.ProjectStatus = "Finished";
            }
            else
            {
                project.ProjectStatus = "Ongoing";
            }

            repositories.SaveChanges();
            var result = new { identifier = project.ProjectId, newStatus = project.ProjectStatus };

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
