using HeavenlyKingdom.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HeavenlyKingdom.Api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static List<User> _users = new();
        private static int _nextId = 1;

        [HttpGet("all")]
        public ActionResult<List<User>> GetAll()
        {
            return Ok(_users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new {Message = $"User with ID {id} not found"} );
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Create([FromBody]User user)
        {
            user.Id = _nextId++;
            _users.Add(user);
            return Created($"/api/user/{user.Id}", user);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == id);
            if (existingUser == null)
                return NotFound(new {Message = $"User with ID {id} not found"});
            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;
            return Ok(existingUser);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                return NotFound(new {Message = $"User with ID {id} not found"});
            _users.Remove(user);
            return NoContent();
        }
    }
}
