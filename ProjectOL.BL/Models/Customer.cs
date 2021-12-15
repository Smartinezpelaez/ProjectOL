using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectOL.BL.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Projects = new HashSet<Project>();
        }

        public int Id { get; set; }
        public string FirstMidName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
    }
}
