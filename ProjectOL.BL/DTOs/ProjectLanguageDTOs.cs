using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProjectOL.BL.DTOs
{
    public enum Levels{ 
        Alto = 5,
        Medio = 3,
        Bajo = 1

    }
    public partial class ProjectLanguageDTOs
    {
        [Required]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("ProjectId")]
        public int? ProjectId { get; set; }

        [Required]
        [JsonProperty("LanguageId")]
        public int? LanguageId { get; set; }

        [Required]
        [JsonProperty("Level")]
        public int? Level { get; set; }

        public virtual LanguageDTOs Language { get; set; }
        public virtual ProjectDTOs Project { get; set; }
    }
}
