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
    public class TimeLogsController : Controller
    {
        private UnitOfWork unitOfWork = new UnitOfWork();

        // GET: TimeLogs
        public ActionResult Index(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Tasks task = unitOfWork.TasksRepository.GetTask(id.Value);

            if (task == null)
            {
                return HttpNotFound();
            }
            IEnumerable<TimeLog> timeLogs = unitOfWork.TimeLogRepository.GetAllTimeLogsUnderTask(task.TasksId);

            return View(new TasksLogViewModel() { Task = task, TimeLogs = timeLogs});
        }

        // GET: TimeLogs/Create
        public ActionResult Create(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(new TimeLog() { TasksId = id.Value });
        }

        // POST: TimeLogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Comments,TimeLogDate,DurationWorked,TasksId")] TimeLog timeLog)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TimeLogRepository.CreateTimeLog(timeLog);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index", "TimeLogs", new { id = timeLog.TasksId});
            }

            ViewBag.TasksId = new SelectList(unitOfWork.TimeLogRepository.GetAllTimeLogs(), "TasksId", "TasksName", timeLog.TasksId);
            return View(timeLog);
        }

        // GET: TimeLogs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeLog timeLog = unitOfWork.TimeLogRepository.GetTimeLog(id.Value);
            if (timeLog == null)
            {
                return HttpNotFound();
            }
            
            return View(timeLog);
        }

        // POST: TimeLogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TimeLogId,Comments,TimeLogDate,DurationWorked,TasksId")] TimeLog timeLog)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.TimeLogRepository.UpdateTimeLog(timeLog);
                unitOfWork.SaveChanges();
                return RedirectToAction("Index", "TimeLogs", new { id = timeLog.TasksId });
            }
            ViewBag.TasksId = new SelectList(unitOfWork.TimeLogRepository.GetAllTimeLogs(), "TasksId", "TasksName", timeLog.TasksId);
            return View(timeLog);
        }

        // GET: TimeLogs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TimeLog timeLog = unitOfWork.TimeLogRepository.GetTimeLog(id.Value);
            if (timeLog == null)
            {
                return HttpNotFound();
            }
            return View(timeLog);
        }

        // POST: TimeLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            int taskId = unitOfWork.TimeLogRepository.GetTimeLog(id).TimeLogId;
            unitOfWork.TimeLogRepository.RemoveTimeLog(id);
            unitOfWork.SaveChanges();
            return RedirectToAction("Index", "TimeLogs", new { id = taskId});
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
