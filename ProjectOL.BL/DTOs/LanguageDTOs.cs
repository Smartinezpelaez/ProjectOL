using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ProjectOL.BL.DTOs
{
    public partial class LanguageDTOs
    {
        [Required]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("Name")]
        public string Name { get; set; }

    }
}
