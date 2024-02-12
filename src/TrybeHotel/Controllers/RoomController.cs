using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("room")]
    public class RoomController : Controller
    {
        private readonly IRoomRepository _repository;
        public RoomController(IRoomRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{HotelId}")]
        public IActionResult GetRoom(int HotelId)
        {
            return Ok(_repository.GetRooms(HotelId));
        }

        [HttpPost]
        public IActionResult PostRoom([FromBody] Room room)
        {
            return Created("", _repository.AddRoom(room));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoom([FromBody] Room room, int id) {
            return StatusCode(204, _repository.UpdateRoom(id, room));
        }

        [HttpDelete("{RoomId}")]
        public IActionResult Delete(int RoomId)
        {
            _repository.DeleteRoom(RoomId);
            return NoContent();
        }
    }
}