using TrybeHotel.Models;
using TrybeHotel.Dto;
using TrybeHotel.Exceptions;

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
                Name = c.Name,
                State = c.State
            }).ToList();

            return query;
        }

        public CityDto AddCity(City city)
        {
            _context.Cities.Add(city);
            _context.SaveChanges();
            var createdCity = _context.Cities.First(c => c.Name == city.Name);

            if (createdCity == null)
                throw new BadRequestException("Name or State is invalid");

            var cityDTO = new CityDto()
            {
                CityId = createdCity.CityId,
                Name = createdCity.Name,
                State = createdCity.State,
            };
            return cityDTO;
        }

        public CityDto UpdateCity(City city)
        {
            var findCity = _context.Cities.First(c => c.CityId == city.CityId);

            if (findCity == null)
                throw new NotFoundException("City not found");

            findCity.Name = city.Name;
            findCity.State = city.State;

            _context.Cities.Update(findCity);
            _context.SaveChanges();

            return new CityDto()
            {
                CityId = findCity.CityId,
                Name = findCity.Name,
                State = findCity.State
            };
            
        }

    }
}