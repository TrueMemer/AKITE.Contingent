using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AKITE.Contingent.Models;
using Microsoft.AspNetCore.Mvc;
using Server.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private readonly StudentsContext DB;

        public StudentsController(StudentsContext ctx)
        {
            DB = ctx;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Student> Get() => DB.Students.ToList();

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = DB.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();
            return new ObjectResult(student);
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Student value)
        {
            if (value == null) return BadRequest();

            DB.Students.Add(value);
            DB.SaveChanges();

            return Ok(value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student value)
        {
            if (value == null) return BadRequest();
            if (!DB.Students.Any(s => s.Id == id)) return NotFound();

            DB.Students.Update(value);
            DB.SaveChanges();

            return Ok(value);
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var student = DB.Students.FirstOrDefault(s => s.Id == id);
            if (student == null) return NotFound();

            DB.Students.Remove(student);
            DB.SaveChanges();

            return Ok(student);
        }
    }
}
