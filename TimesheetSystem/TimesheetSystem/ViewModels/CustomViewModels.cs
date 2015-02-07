using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TimesheetSystem.DAL.Models;

namespace TimesheetSystem.ViewModels
{
    public class ProjectTasksViewModel
    {
        [Required]
        public Project Project { get; set; }

        public IEnumerable<Tasks> Tasks { get; set; }
    }

    public class TasksLogViewModel
    {
        [Required]
        public Tasks Task { get; set; }

        [Required]
        public IEnumerable<TimeLog> TimeLogs { get; set; }
    }
}