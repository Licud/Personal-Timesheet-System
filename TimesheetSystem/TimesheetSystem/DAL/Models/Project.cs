using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TimesheetSystem.DAL.Models
{
    public class Project: IValidatableObject
    {
        public int ProjectId { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string ProjectName { get; set; }

        [Required]
        [Display(Name = "Project Type")]
        public string ProjectType { get; set; }

        [Required]
        [Display(Name = "Project Start Date")]
        [DataType(DataType.Date)]
        public DateTime ProjectDateStarted { get; set; }

        [Required]
        [Display(Name = "Project End Date")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> res = new List<ValidationResult>();

            if (EstimatedProjectDateEnd < ProjectDateStarted)
            {
                res.Add(new ValidationResult("The Project End Date must be greater than the Project Start Date"));
            }

            return res;
        }
    }
}