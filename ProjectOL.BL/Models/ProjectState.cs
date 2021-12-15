using System;
using System.Collections.Generic;



namespace ProjectOL.BL.Models
{
    public partial class ProjectState
    {
        public ProjectState()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Project> Projects { get; set; }

    }
}
