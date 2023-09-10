namespace FlightBookingAPI.Models
{
    public class Airport
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public Airport()
        {
        }

        public Airport(string code, string name, string city, string country)
        {
            Code = code;
            Name = name;
            City = city;
            Country = country;
        }
    }
}
