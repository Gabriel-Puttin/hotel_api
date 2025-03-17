using Microsoft.AspNetCore.Mvc;
using TrybeHotel.Models;
using TrybeHotel.Repository;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace TrybeHotel.Controllers
{
    [ApiController]
    [Route("user")]

    public class UserController : Controller
    {
        private readonly IUserRepository _repository;
        public UserController(IUserRepository repository)
        {
            _repository = repository;
        }
        
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult GetUsers(){
            return Ok(_repository.GetUsers());
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Add([FromBody] UserDtoInsert user)
        {
            try
            {
                return Created("", _repository.Add(user));
            }
            catch (ConflictException e)
            {
                return Conflict(new { message = e.Message });
            }
        }
    }
}