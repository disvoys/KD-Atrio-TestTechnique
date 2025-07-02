using KdAtrio.Converters;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KdAtrio.Models
{
    public class Person
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string FirstName { get; set; }

        [Required, StringLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        [SwaggerSchema(Description = "Format: dd/MM/yyyy")]
        public DateTime DateOfBirth { get; set; }

        public ICollection<Job> Jobs { get; set; } = new List<Job>();

        public ICollection<Job> ActualJobs => Jobs.Where(j => j.EndDate == null).ToList();


        public int Age => (int)((DateTime.Now - DateOfBirth).TotalDays / 365.25);
    }
}
