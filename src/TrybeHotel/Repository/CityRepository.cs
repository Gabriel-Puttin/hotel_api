using TrybeHotel.Models;
using TrybeHotel.Dto;

namespace TrybeHotel.Repository
{
    public class CityRepository : ICityRepository
    {
        protected readonly ITrybeHotelContext _context;
        public CityRepository(ITrybeHotelContext context)
        {
            _context = context;
        }

        public IEnumerable<CityDto> GetCities()
        {
            var query = _context.Cities.Select(c => new CityDto()
            {
                CityId = c.CityId,
                Name = c.Name
            }).ToList();

            return query;
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            var createdCity = _context.Cities.First(c => c.Name == city.Name);
            var cityDTO = new CityDto()
            {
                CityId = createdCity.CityId,
                Name = createdCity.Name
            };
            return cityDTO;
        }
    }
}
