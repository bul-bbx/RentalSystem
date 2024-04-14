using RentalSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Data.Models.Entities
{
    public class CarServiceModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        public int PassengerSeats { get; set; }

        public string Description { get; set; }

        [Required]
        public decimal PricePerDay { get; set; }
        [Required]
        public string Fuel { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
