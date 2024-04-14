namespace RentalSystem.Data.Models
{
    public class CarReservationViewModel
    {
        public IEnumerable<CarListingViewModel> Cars { get; set; }
        public IEnumerable<ReservationListingViewModel> Reservations { get; set; }
    }
}
