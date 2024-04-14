namespace RentalSystem.Data.Models
{
    public class AvailableCarViewModel
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int PassengerSeats { get; set; }
        public string Description { get; set; }
        public decimal PricePerDay { get; set; }
        public string Fuel {  get; set; }
    }
}
