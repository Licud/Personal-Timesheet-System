using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TimesheetSystem.DAL.Models
{
    public class Tasks : IValidatableObject
    {
        public int TasksId { get; set; }

        [Required]
        [Display(Name = "Task Name")]
        public string TasksName { get; set; }

        [Required]
        [Display(Name = "Task Type")]
        public string TaskType { get; set; }

        [Required]
        [Display(Name = "Task Start Date")]
        [DataType(DataType.Date)]
        public DateTime TaskStartDate { get; set; }

        [Required]
        [Display(Name = "Task End Date")]
        [DataType(DataType.Date)]
        public DateTime EstimatedTaskDateEnd { get; set; }

        [Display(Name = "Task Duration")]
        public int TaskDuration { get; set; }

        [Display(Name = "Status")]
        public string TasksStatus { get; set; }

        public int ProjectId { get; set; }

        public virtual IEnumerable<TimeLog> TimeLogs { get; set; }

        public virtual Project Project { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();

            if (EstimatedTaskDateEnd < TaskStartDate)
            {
                res.Add(new ValidationResult("The Task End Date must be greater than the Task Start Date"));
            }

            return res;
        }
    }
}