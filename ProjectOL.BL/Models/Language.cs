using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectOL.BL.Models
{
    public partial class Language
    {
        public Language()
        {
            ProjectLanguages = new HashSet<ProjectLanguage>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<ProjectLanguage> ProjectLanguages { get; set; }
    }
}
