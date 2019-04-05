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
            DB.Database.EnsureCreated();
            if (DB.Students.Any()) return;
            DB.Students.Add(
                new Student
                {
                    CaseNum = 1, Birthday = DateTime.Now, GroupIndex = 1, Gender = 0, FirstName = "Иван",
                    LastName = "Иванов", MidName = "Иванович", AttNum = "1", CertNum = "1"
                });
            DB.Students.Add(
                new Student
                {
                    CaseNum = 2, Birthday = DateTime.Now, GroupIndex = 1, Gender = 0, FirstName = "Петр",
                    LastName = "Петров", MidName = "Петрович", AttNum = "2", CertNum = "2"
                });
            DB.Students.Add(
                new Student
                {
                    CaseNum = 3, Birthday = DateTime.Now, GroupIndex = 2, Gender = 0, FirstName = "Сидоров",
                    LastName = "Никита", MidName = "Федорович", AttNum = "3", CertNum = "3"
                });
            DB.Students.Add(
                new Student
                {
                    CaseNum = 4, Birthday = DateTime.Now, GroupIndex = 3, Gender = 1,
                    FirstName = "Алиса", LastName = "Рейх", MidName = "Руслановна", AttNum = "4",
                    CertNum = "4"
                });
            DB.Students.Add(
                new Student
                {
                    CaseNum = 5, Birthday = DateTime.Now, GroupIndex = 3, Gender = 1,
                    FirstName = "Анастасия", LastName = "Лис", MidName = "Александровна",
                    AttNum = "5", CertNum = "5"
                });
            DB.Students.Add(
                new Student
                {
                    CaseNum = 6, Birthday = DateTime.Now, GroupIndex = 4, Gender = 0,
                    FirstName = "Александр", LastName = "Пирогов", MidName = "Викторович",
                    AttNum = "6", CertNum = "6"
                });
            DB.Students.Add(new Student
            {
                CaseNum = 7, Birthday = DateTime.Now, GroupIndex = 4, Gender = 0,
                FirstName = "Евгений", LastName = "Титаренко", MidName = "Андреевич",
                AttNum = "7", CertNum = "7"
            });
            DB.SaveChanges();
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
        [HttpPut]
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
