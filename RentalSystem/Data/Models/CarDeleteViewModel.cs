using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using RentalSystem.Data.Mapping;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Models
{
    public class CarDeleteViewModel : IMapWith<CarServiceModel>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Brand { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Model { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required]
        [Display(Name = "Passenger Seats")]
        public int PassengerSeats { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Price Per Day")]
        public decimal PricePerDay { get; set; }
        [Required]
        [Display(Name = "Fuel")]
        public string Fuel { get; set; }

    }
}
