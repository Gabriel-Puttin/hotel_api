using Microsoft.EntityFrameworkCore;
using TrybeHotel.Models;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;

namespace TrybeHotel.Repository
{
    public class BookingRepository : IBookingRepository
    {
        protected readonly ITrybeHotelContext _context;
        public BookingRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public BookingResponse Add(BookingDtoInsert booking, string email)
        {
            Room room = GetRoomById(booking.RoomId);
            User? user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (booking.GuestQuant > room.Capacity)
            {
                throw new BadRequestException("Guest quantity over room capacity");
            }

            var newBooking = new Booking()
            {
                RoomId = booking.RoomId,
                GuestQuant = booking.GuestQuant,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                UserId = user!.UserId,
            };

            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return GetBooking(newBooking.BookingId, email);
        }

        public BookingResponse GetBooking(int bookingId, string email)
        {
            User? user = _context.Users.FirstOrDefault(user => user.Email == email);
            Booking? booking = _context.Bookings.FirstOrDefault(b => b.BookingId == bookingId);
            Room? room = _context.Rooms.Include(r => r.Hotel).ThenInclude(h => h!.City).FirstOrDefault(r => r.RoomId == booking!.RoomId);

            if (booking!.UserId != user!.UserId)
            {
                throw new UnauthorizedException();
            }

            return new BookingResponse
            {
                BookingId = booking.BookingId,
                CheckIn = booking.CheckIn,
                CheckOut = booking.CheckOut,
                GuestQuant = booking.GuestQuant,
                Room = new RoomDto
                {
                    RoomId = room!.RoomId,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Image = room.Image,
                    Hotel = new HotelDto
                    {
                        HotelId = room.HotelId,
                        Name = room.Hotel!.Name,
                        Address = room.Hotel.Address,
                        CityId = room.Hotel.CityId,
                        CityName = room.Hotel.City!.Name,
                        State = room.Hotel.City!.State,
                    },
                },
            };
        }

        public Room GetRoomById(int RoomId)
        {
            Room? query = _context.Rooms.First(r => r.RoomId == RoomId);

            return query!;
        }

    }

}