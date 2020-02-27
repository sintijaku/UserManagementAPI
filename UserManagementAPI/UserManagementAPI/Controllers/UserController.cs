using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService context)
        {
            _service = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var items = _service.GetAllUsers();
            return Ok(items);
        }

        [HttpGet("{id}")]
        public  ActionResult<User> GetUser(int id)
        {
            var item = _service.GetById(id);

            if (item==null)
            {
                return BadRequest();
            }
            return Ok(item);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody]User user)
        {

            var existingUser=_service.GetById(id);
            if (id != user.ID)
            {
                return BadRequest();
            }
            if (existingUser!=null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.IsActive = user.IsActive;
            }
            return Ok(user);
        }


        [HttpPost]
        public ActionResult PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var item = _service.Add(user);
            return CreatedAtAction("Get", new { id = item.ID }, item);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _service.GetById(id);

            if (existingUser==null)
            {
                return NotFound();
            }

            _service.Remove(id);
            return Ok();
        }

    }
}