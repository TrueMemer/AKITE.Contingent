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
    public class GroupsController : ControllerBase
    {
        private readonly StudentsContext DB;

        public GroupsController(StudentsContext ctx)
        {
            DB = ctx;
        }

        // GET: api/Groups
        [HttpGet]
        public IEnumerable<Group> Get() => DB.Groups.ToList();

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var group = DB.Groups.SingleOrDefault(g => g.Id == id);
            if (group == null) return NotFound();
            return Ok(group);
        }

        // POST: api/Groups
        [HttpPost]
        public IActionResult Post([FromBody] Group value)
        {
            if (value == null) return BadRequest();

            DB.Groups.Add(value);
            DB.SaveChanges();

            return Ok(value);
        }

        //// PUT: api/Groups/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE: api/ApiWithActions/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
