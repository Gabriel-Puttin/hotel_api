using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("booking")]

    public class BookingController : Controller
    {
        private readonly IBookingRepository _repository;
        public BookingController(IBookingRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Authorize(Policy = "Client")]
        public IActionResult Add([FromBody] BookingDtoInsert bookingInsert)
        {
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                string? email = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                return Created("", _repository.Add(bookingInsert, email!));
            }
            catch (BadRequestException e)
            {
                return BadRequest(new { message = e.Message });
            }
        }


        [HttpGet("{Bookingid}")]
        [Authorize(Policy = "Client")]
        public IActionResult GetBooking(int Bookingid)
        {
            try
            {
                var token = HttpContext.User.Identity as ClaimsIdentity;
                string? email = token?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
                return Ok(_repository.GetBooking(Bookingid, email!));
            }
            catch (UnauthorizedException e)
            {
                return Unauthorized(new { message = e.Message });
            }
        }
    }
}
