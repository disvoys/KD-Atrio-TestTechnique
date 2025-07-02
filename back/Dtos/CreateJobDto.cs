using KdAtrio.Converters;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KdAtrio.Dtos
{
    public class CreateJobDto
    {
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
    }
}
