using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class HotelRepository : IHotelRepository
    {
        protected readonly ITrybeHotelContext _context;
        public HotelRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<HotelDto> GetHotels()
        {
            var query = _context.Hotels.Select(h => new HotelDto()
            {
                HotelId = h.HotelId,
                Name = h.Name,
                Address = h.Address,
                CityId = h.CityId,
                CityName = h.City!.Name,
                State = h.City!.State,
            }).ToList();

            return query;
        }

        public HotelDto AddHotel(Hotel hotel)
        {
            City city = _context.Cities.First(city => city.CityId == hotel.CityId);
            _context.Hotels.Add(hotel);
            _context.SaveChanges();

            HotelDto hotelDTO = new HotelDto()
            {
                HotelId = hotel.HotelId,
                Name = hotel.Name,
                Address = hotel.Address,
                CityId = hotel.CityId,
                CityName = city.Name,
                State = city.State,
            };

            return hotelDTO;
        }
    }
}
