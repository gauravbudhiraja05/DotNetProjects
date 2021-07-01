using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UserCRUDOperations.Context;
using UserCRUDOperations.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserCRUDOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly DBContext _dBContext;
        public UserController(DBContext dBContext)
        {
            _dBContext = dBContext;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            var data = _dBContext.User.ToList();
            return data;
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            var data = _dBContext.User.ToList().Where(u => u.Id == id).FirstOrDefault();
            return data;
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            var data = _dBContext.User.Add(user);
            _dBContext.SaveChanges();
            return Ok();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            var data = _dBContext.User.Update(user);
            _dBContext.SaveChanges();
            return Ok();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = _dBContext.User.Where(a => a.Id == id).FirstOrDefault();
            _dBContext.User.Remove(user);
            _dBContext.SaveChanges();
            return Ok();
        }
    }
}
