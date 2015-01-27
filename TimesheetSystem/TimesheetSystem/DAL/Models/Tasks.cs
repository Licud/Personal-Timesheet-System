using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetSystem.DAL.Models
{
    public class Tasks
    {
        public int TasksId { get; set; }

        [Display(Name = "Task Name")]
        public string TasksName { get; set; }

        [Display(Name = "Task Type")]
        public string TaskType { get; set; }

        [Display(Name = "Task Start Date")]
        public DateTime TaskStartDate { get; set; }

        [Display(Name = "Task End Date")]
        public DateTime EstimatedTaskDateEnd { get; set; }

        [Display(Name = "Task Duration")]
        public int TaskDuration { get; set; }

        [Display(Name = "Status")]
        public int TasksStatus { get; set; }

        public int ProjectId { get; set; }

        public virtual IEnumerable<TimeLog> TimeLogs { get; set; }

        public virtual Project Project { get; set; }
    }
}