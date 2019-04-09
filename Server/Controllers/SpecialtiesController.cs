using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKITE.Contingent.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly StudentsContext DB;

        public SpecialtiesController(StudentsContext ctx)
        {
            DB = ctx;
        }

        // GET: api/Specialties
        [HttpGet]
        public IEnumerable<Specialty> Get() => DB.Specialties.ToList();

        // GET: api/Specialties/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var spec = DB.Specialties.SingleOrDefault(s => s.Id == id);
            if (spec == null) return NotFound();
            return Ok(spec);
        }

        // POST: api/Specialties
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Specialties/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
