using TrybeHotel.Models;
using TrybeHotel.Dto;
using Microsoft.EntityFrameworkCore;

namespace TrybeHotel.Repository
{
    public class RoomRepository : IRoomRepository
    {
        protected readonly ITrybeHotelContext _context;
        public RoomRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<RoomDto> GetRooms(int HotelId)
        {
            var query = _context.Rooms
            .Include(r => r.Hotel)
            .ThenInclude(h => h!.City)
            .Select(r => new RoomDto
            {
                RoomId = r.RoomId,
                Name = r.Name,
                Capacity = r.Capacity,
                Image = r.Image,
                Hotel = new HotelDto
                {
                    HotelId = r.Hotel!.HotelId,
                    Name = r.Hotel.Name,
                    Address = r.Hotel.Address,
                    CityId = r.Hotel.City!.CityId,
                    CityName = r.Hotel.City.Name,
                    State = r.Hotel.City.State
                }
            });

            return query;
        }

        public RoomDto AddRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
            RoomDto findRoom = GetRooms(room.HotelId).First(q => q.Name == room.Name);
            return findRoom;
        }

        public RoomDto UpdateRoom(int id, Room room)
        {
            var findRoom = _context.Rooms.Where(r => r.RoomId == id).FirstOrDefault();
            findRoom!.Name = room.Name;
            findRoom.Capacity = room.Capacity;
            findRoom.Image = room.Image;
            findRoom.Hotel = room.Hotel;
            _context.Rooms.Update(findRoom);
            _context.SaveChanges();
            return GetRooms(id).First(q => q.Name == room.Name); ;
        }

        public void DeleteRoom(int RoomId)
        {
            var room = _context.Rooms.Where(r => r.RoomId == RoomId).FirstOrDefault();
            _context.Rooms.Remove(room!);
            _context.SaveChanges();
        }

    }
}
