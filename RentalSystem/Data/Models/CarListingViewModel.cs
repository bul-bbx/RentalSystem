using RentalSystem.Data.Mapping;
using RentalSystem.Data.Models.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Data.Models
{
    public class CarListingViewModel : IMapWith<CarServiceModel>
    {
        public string Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public int PassengerSeats { get; set; }

        public string Description { get; set; }

        public decimal PricePerDay { get; set; }
        public string Fuel { get; set; }
    }
}
