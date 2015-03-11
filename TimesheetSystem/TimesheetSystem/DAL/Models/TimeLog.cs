using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimesheetSystem.DAL.Models
{
    public class TimeLog
    {
        public int TimeLogId { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Log Date")]
        [DataType(DataType.Date)]
        public DateTime TimeLogDate { get; set; }

        [Display(Name = "Work Duration")]
        public double DurationWorked { get; set; }

        public int TasksId { get; set; }

        [ForeignKey("TasksId")]
        public virtual Tasks Task { get; set; }
    }
}