using System;
using System.Collections.Generic;

#nullable disable

namespace ProjectOL.BL.Models
{
    public partial class ProjectLanguage
    {
        public int Id { get; set; }
        public int? ProjectId { get; set; }
        public int? LanguageId { get; set; }
        public int? Level { get; set; }

        public virtual Language Language { get; set; }
        public virtual Project Project { get; set; }
    }
}
