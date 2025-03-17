using System.Net.Http;
using System.Security.Cryptography;
using TrybeHotel.Dto;
using TrybeHotel.Repository;


namespace TrybeHotel.Services
{
    public class GeoService : IGeoService
    {
        private readonly HttpClient _client;
        private const string _baseUrl = "https://nominatim.openstreetmap.org/status.php?format=json";
        public GeoService(HttpClient client)
        {
            _client = client;
            _client.BaseAddress = new Uri(_baseUrl);
        }

        public async Task<object> GetGeoStatus()
        {
            var response = await _client.GetAsync(_baseUrl);

            if (!response.IsSuccessStatusCode)
                return default(Object)!;

            var content = await response.Content.ReadFromJsonAsync<object>();
            return content!;
        }

        public async Task<GeoDtoResponse> GetGeoLocation(GeoDto geoDto)
        {
            var url = $"https://nominatim.openstreetmap.org/search?street={geoDto.Address}&city={geoDto.City}&country=Brazil&state={geoDto.State}&format=json&limit=1";
            var response = await _client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
                return default(GeoDtoResponse)!;

            var content = await response.Content.ReadFromJsonAsync<List<GeoDtoResponse>>();
            return content![0];
        }

        public async Task<List<GeoDtoHotelResponse>> GetHotelsByGeo(GeoDto geoDto, IHotelRepository repository)
        {
            var geoHotels = new List<GeoDtoHotelResponse>();
            IEnumerable<HotelDto> hotels = repository.GetHotels();
            foreach (var hotel in hotels)
            {
                GeoDto newHotel = new GeoDto { Address = hotel.Address, City = hotel.CityName, State = hotel.State };

                var geoHotel = await GetGeoLocation(newHotel);
                var geo = await GetGeoLocation(geoDto);
                var distance = CalculateDistance(geoHotel.lat!, geoHotel.lon!, geo.lat!, geo.lon!);

                var geoHotelResponse = new GeoDtoHotelResponse
                {
                    HotelId = hotel.HotelId,
                    Name = hotel.Name,
                    Address = hotel.Address,
                    CityName = hotel.CityName,
                    State = hotel.State,
                    Distance = distance
                };
                geoHotels.Add(geoHotelResponse);
            }

            return geoHotels;

        }

        public int CalculateDistance(string latitudeOrigin, string longitudeOrigin, string latitudeDestiny, string longitudeDestiny)
        {
            double latOrigin = double.Parse(latitudeOrigin.Replace('.', ','));
            double lonOrigin = double.Parse(longitudeOrigin.Replace('.', ','));
            double latDestiny = double.Parse(latitudeDestiny.Replace('.', ','));
            double lonDestiny = double.Parse(longitudeDestiny.Replace('.', ','));
            double R = 6371;
            double dLat = radiano(latDestiny - latOrigin);
            double dLon = radiano(lonDestiny - lonOrigin);
            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(radiano(latOrigin)) * Math.Cos(radiano(latDestiny)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double distance = R * c;
            return int.Parse(Math.Round(distance, 0).ToString());
        }

        public double radiano(double degree)
        {
            return degree * Math.PI / 180;
        }

    }
}