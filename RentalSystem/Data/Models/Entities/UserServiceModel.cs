using Microsoft.AspNetCore.Identity;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Mapping;
using System.ComponentModel.DataAnnotations;

namespace RentalSystem.Data.Models.Entities
{
    public class UserServiceModel : IdentityUser, IMapWith<User>
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 10)]
        public string EGN { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
    }
}
