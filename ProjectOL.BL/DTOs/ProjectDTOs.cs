using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProjectOL.BL.DTOs
{
    public partial class ProjectDTOs
    {
        [Required]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [Required]
        [JsonProperty("StartDate")]
        public string StartDate { get; set; }

        [Required]
        [JsonProperty("EndDate")]
        public string EndDate { get; set; }

        [Required]
        [JsonProperty("Price")]
        public double? Price { get; set; }

        [Required]
        [JsonProperty("Hours")]
        public double? Hours { get; set; }

        [Required]
        [Display(Name = "Project State")]
        public int? ProjectStateId { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int? CustomerId { get; set; }

        public virtual CustomerDTOs Customer { get; set; }
        public virtual ProjectStateDTOs ProjectState { get; set; }
        
    }
}
