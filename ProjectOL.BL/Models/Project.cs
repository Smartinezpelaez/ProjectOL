using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectOL.BL.Models
{
    public partial class Project
    {
        public Project()
        {
            ProjectLanguages = new HashSet<ProjectLanguage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public double? Price { get; set; }
        public double? Hours { get; set; }
        public int? ProjectStateId { get; set; }
        public int? CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProjectState ProjectState { get; set; }
        public virtual ICollection<ProjectLanguage> ProjectLanguages { get; set; }
    }
}
