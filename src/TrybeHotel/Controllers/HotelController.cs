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

        // 5. Desenvolva o endpoint POST /hotel
        [HttpPost]
        public IActionResult PostHotel([FromBody] Hotel hotel)
        {
            throw new NotImplementedException();
        }


    }
}