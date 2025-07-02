using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KdAtrio.Data;
using KdAtrio.Models;
using KdAtrio.Dtos;
using System.Globalization;

namespace KdAtrio.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonController(AppDbContext context)
        {
            _context = context;
        }


        // POST api/person
        [HttpPost]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonDto input)
        {
            var age = (int)((DateTime.Now - input.DateOfBirth).TotalDays / 365.25);
            if (age >= 150)
                return BadRequest("La personne doit avoir moins de 150 ans.");

            var person = new Person
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                DateOfBirth = input.DateOfBirth
            };

            _context.Persons.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, person);
        }

        // GET api/person
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAllPersons()
        {
            var persons = await _context.Persons
                .Include(p => p.Jobs)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();

            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetPersonById(int id)
        {
            var person = await _context.Persons
                .Include(p => p.Jobs)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (person == null)
                return NotFound();

            return Ok(person);
        }


        // POST api/person/{id}/jobs
        [HttpPost("{id}/jobs")]
        public async Task<IActionResult> AddJobToPerson(int id, [FromBody] CreateJobDto input)
        {
            var person = await _context.Persons.Include(p => p.Jobs).FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
                return NotFound("Personne non trouvée.");

            if (input.StartDate == default)
                return BadRequest("La date de début est obligatoire.");

            if (input.EndDate != null && input.EndDate <= input.StartDate)
                return BadRequest("La date de fin doit être supérieure à la date de début.");

            var job = new Job
            {
                CompanyName = input.CompanyName,
                Position = input.Position,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                PersonId = person.Id
            };

            person.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPersonById), new { id = person.Id }, job);
        }


        // GET api/person/bycompany?companyName=XYZ
        [HttpGet("bycompany")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonsByCompany([FromQuery] string companyName)
        {
            if (string.IsNullOrWhiteSpace(companyName))
                return BadRequest("Le nom de l'entreprise est requis.");

            var persons = await _context.Persons
                .Where(p => p.Jobs.Any(j => j.CompanyName.ToLower() == companyName.ToLower()))
                .Include(p => p.Jobs)
                .ToListAsync();

            return Ok(persons);
        }


        // GET api/person/{id}/jobsbetween/{10/01/2024}/{10/02/2025}
        [HttpGet("{id}/jobsbetween")]
        public async Task<ActionResult<IEnumerable<Job>>> GetJobsBetweenDates(int id, [FromQuery] string start, [FromQuery] string end)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
                return NotFound();

            if (!DateTime.TryParseExact(start, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate)
             || !DateTime.TryParseExact(end, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime endDate))
            {
                return BadRequest("Dates must be in format dd/MM/yyyy");
            }

            var jobs_ = _context.Jobs;

            var jobs = await _context.Jobs
                .Where(j => j.PersonId == id
                    && j.StartDate <= endDate
                    && (j.EndDate == null || j.EndDate >= startDate))
                .ToListAsync();

            return Ok(jobs);
        }

    }
}
