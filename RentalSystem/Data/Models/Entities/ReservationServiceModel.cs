using RentalSystem.Data.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RentalSystem.Data.Models.Entities
{
    public class ReservationServiceModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public string CarId { get; set; }

        public Car Car { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
