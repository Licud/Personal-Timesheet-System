using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.DAL.Repositories
{
    public class TimeLogRepository
    {
        private ApplicationDbContext context;

        public TimeLogRepository(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        public IEnumerable<TimeLog> GetAllTimeLogs()
        {
            return context.TimeLog.ToList();
        }

        public IEnumerable<TimeLog> GetAllTimeLogsUnderTask(int id)
        {
            return context.TimeLog.Where(tl => tl.TasksId == id).ToList();
        }

        public TimeLog GetTimeLog(int id)
        {
            return context.TimeLog.Find(id);
        }

        public void CreateTimeLog(TimeLog _timeLog)
        {
            context.TimeLog.Add(_timeLog);
        }

        public void UpdateTimeLog(TimeLog _timeLog)
        {
            context.Entry(_timeLog).State = EntityState.Modified;
        }

        public void RemoveTimeLog(int id)
        {
            TimeLog logToBeRemoved = context.TimeLog.Find(id);
            context.TimeLog.Remove(logToBeRemoved);
        }
    }
}