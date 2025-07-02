using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using KdAtrio.Converters;
using Swashbuckle.AspNetCore.Annotations;

namespace KdAtrio.Dtos
{
    public class CreatePersonDto
    {
        [Required, StringLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, StringLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        [SwaggerSchema(Description = "Format: dd/MM/yyyy")]
        public DateTime DateOfBirth { get; set; }
    }
}
