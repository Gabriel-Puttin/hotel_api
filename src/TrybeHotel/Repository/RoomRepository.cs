using TrybeHotel.Models;
using TrybeHotel.Dto;

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
            var query = _context.Rooms.Select(r => new RoomDto()
            {
                RoomId = r.RoomId,
                Name = r.Name,
                Capacity = r.Capacity,
                Image = r.Image,
                Hotel = _context.Hotels.Select(h => new HotelDto()
                {
                    HotelId = h.HotelId,
                    Name = h.Name,
                    Address = h.Address,
                    CityId = h.CityId,
                    CityName = _context.Cities.First(c => c.CityId == h.CityId).Name
                }).First(h => h.HotelId == r.HotelId)
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

        public RoomDto UpdateRoom(int id, Room room) {
            var findRoom = _context.Rooms.Where(r => r.RoomId == id).FirstOrDefault();
            findRoom!.Name = room.Name;
            findRoom.Capacity = room.Capacity;
            findRoom.Image = room.Image;
            findRoom.Hotel = room.Hotel;
            _context.Rooms.Update(findRoom);
            _context.SaveChanges();
            return GetRooms(id).First(q => q.Name == room.Name);;
        }

        public void DeleteRoom(int RoomId)
        {
            var room = _context.Rooms.Where(r => r.RoomId == RoomId).FirstOrDefault();
            _context.Rooms.Remove(room!);
            _context.SaveChanges();
        }
    }
}
