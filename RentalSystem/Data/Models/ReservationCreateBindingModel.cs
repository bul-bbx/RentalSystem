using RentalSystem.Data.Mapping;
using RentalSystem.Data.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Data.Models
{
    public class ReservationCreateBindingModel : IMapWith<ReservationServiceModel>
    {
        [Required]
        public string CarId { get; set; }

        [Required]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
    }
}
