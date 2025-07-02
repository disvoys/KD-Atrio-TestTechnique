using KdAtrio.Converters;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KdAtrio.Models
{
    public class Job
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string CompanyName { get; set; } = null!;

        [Required, StringLength(100)]
        public string Position { get; set; } = null!;

        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        [SwaggerSchema(Description = "Format: dd/MM/yyyy")]
        public DateTime StartDate { get; set; }

        [JsonConverter(typeof(DateOnlyJsonConverter))]
        [SwaggerSchema(Description = "Format: dd/MM/yyyy")]
        public DateTime? EndDate { get; set; }

        // Foreign Key vers Person
        public int PersonId { get; set; }


        [JsonIgnore]
        public Person Person { get; set; } = null!;
    }
}
