using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("hotel")]
    public class HotelController : Controller
    {
        private readonly IHotelRepository _repository;

        public HotelController(IHotelRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetHotels()
        {
            return Ok(_repository.GetHotels());
        }

        [HttpPost]
        public IActionResult PostHotel([FromBody] Hotel hotel)
        {
            return Created("", _repository.AddHotel(hotel));
        }
    }
}
