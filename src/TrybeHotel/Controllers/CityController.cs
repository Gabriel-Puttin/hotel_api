using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("city")]
    public class CityController : Controller
    {
        private readonly ICityRepository _repository;
        public CityController(ICityRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetCities()
        {
            return Ok(_repository.GetCities());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult PostCity([FromBody] City city)
        {
            try
            {
                return Created("", _repository.AddCity(city));

            }
            catch (BadRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }

        [HttpPut]
        [AllowAnonymous]
        public IActionResult PutCity([FromBody] City city)
        {
            try
            {
                return Ok(_repository.UpdateCity(city));
            }
            catch (NotFoundException e)
            {
                return NotFound(new { message = e.Message });
            }
        }
    }
}
