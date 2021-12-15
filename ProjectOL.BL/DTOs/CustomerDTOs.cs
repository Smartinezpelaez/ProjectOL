using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;


namespace ProjectOL.BL.DTOs
{
    public partial class CustomerDTOs
    {

        [Required]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [Required]
        [JsonProperty("FirstMidName")]
        public string FirstMidName { get; set; }

        [Required]
        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [Required]
        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [Required]
        [JsonProperty("Location")]
        public string Location { get; set; }

        [Required]
        [JsonProperty("Email")]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        [JsonProperty("FullName")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

    }
}
