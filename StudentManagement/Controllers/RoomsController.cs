using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagement.Entities;

namespace StudentManagement.Controllers
{
    [Route("api/rooms")]
    [ApiController]
    public class RoomsController(StudentManagementContext studentManagementContext) : ControllerBase
    {
        private readonly StudentManagementContext _studentManagementContext = studentManagementContext;

        [HttpGet]
        [Route("hello")]

        public IActionResult HelloWorld()
        {
            return Ok("Hello World");
        }

        [HttpGet]
        public IActionResult GetRooms()
        {
            var rooms = _studentManagementContext.Rooms.ToList();
            return Ok(rooms);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetRooms([FromRoute] Guid id)
        {
            var rooms = _studentManagementContext.Rooms.Where(x => x.Id == id).FirstOrDefault();
            if (rooms == null)
            {
                return NotFound("Khong tim thay phong");
            }
            return Ok(rooms);
        }

        [HttpPost]
        public IActionResult CreateRooms([FromBody] Room room)
        {
            _studentManagementContext.Add(room);
            var result = _studentManagementContext.SaveChanges();
            if (result > 0)
            {
                return StatusCode(201, result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteRooms([FromRoute] Guid id)
        {
            var room = _studentManagementContext.Rooms.Where(x => x.Id == id).FirstOrDefault();
            if (room == null)
            {
                return NotFound();
            }
            _studentManagementContext.Remove(room);
            var result = _studentManagementContext.SaveChanges();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }

        [HttpPatch]
        [Route("{id}")]
        public IActionResult UpdateRooms([FromRoute] Guid id, [FromBody] Room updatedRoom)
        {
            var room = _studentManagementContext.Rooms.Where(x => x.Id == id).FirstOrDefault();
            if (room == null)
            {
                return NotFound();
            }

            room.Name = updatedRoom.Name;
            _studentManagementContext.Update(room);
            var result = _studentManagementContext.SaveChanges();
            if (result > 0)
            {
                return NoContent();
            }
            return BadRequest();
        }
    }
}
