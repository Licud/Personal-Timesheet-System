using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimesheetSystem.DAL.Models
{
    public class Project
    {
        public int ProjectId { get; set; }

        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime ProjectDateStarted { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime EstimatedProjectDateEnd { get; set; }

        [Display(Name = "Estimated Duration")]
        public int EstimatedDuration { get; set; }

        [Display(Name = "Total Duration")]
        public int TotalDuration { get; set; }

        [Display(Name = "Status")]
        public string ProjectStatus { get; set; }

        [ForeignKey("TasksId")]
        public virtual IEnumerable<Tasks> Tasks { get; set; }
    }
}