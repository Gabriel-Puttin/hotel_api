using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public interface IRoomRepository
    {
        IEnumerable<RoomDto> GetRooms(int HotelId);
        RoomDto AddRoom(Room room);
        RoomDto UpdateRoom(int id, Room room);
        void DeleteRoom(int RoomId);
    }
}
