using TrybeHotel.Models;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;

namespace TrybeHotel.Repository
{
    public class UserRepository : IUserRepository
    {
        protected readonly ITrybeHotelContext _context;
        public UserRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<UserDto> GetUsers()
        {
            IQueryable<UserDto> query = _context.Users.Select(u => new UserDto()
            {
                UserId = u.UserId,
                Name = u.Name,
                Email = u.Email,
                UserType = u.UserType,
            });

            return query!;
        }

        public UserDto GetUserById(int userId)
        {
            User? user = _context.Users.FirstOrDefault(u => u.UserId == userId);
            return new UserDto
            {
                UserId = user!.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType,
            };
        }

        public UserDto Login(LoginDto login)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);
            if (user == null)
            {
                throw new UnauthorizedException("Incorrect e-mail or password");
            }

            return new UserDto
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType,
            };

        }
        public UserDto Add(UserDtoInsert user)
        {
            var userExists = _context.Users.FirstOrDefault(u => u.Email == user.Email);
            if (userExists != null)
            {
                throw new ConflictException("User email already exists");
            }

            User newUser = new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = user.Password,
                UserType = "client"
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();

            return new UserDto
            {
                UserId = newUser.UserId,
                Name = newUser.Name,
                Email = newUser.Email,
                UserType = newUser.UserType,
            };
        }

        public UserDto GetUserByEmail(string userEmail)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
            return new UserDto
            {
                UserId = user!.UserId,
                Name = user.Name,
                Email = user.Email,
                UserType = user.UserType,
            };
        }

    }
}
