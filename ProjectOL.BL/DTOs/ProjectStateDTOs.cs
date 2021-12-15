using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ProjectOL.BL.DTOs
{
    public partial class ProjectStateDTOs
    {
        [Required]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Display(Name = "State Name")]
        public string Name { get; set; }
    }
}
