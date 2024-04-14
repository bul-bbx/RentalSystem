using RentalSystem.Data.Entities;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Services
{
    public interface IReservationService
    {
        Task<ReservationServiceModel> AddRequestAsync(ReservationServiceModel requestModel);
        Task<bool> Create(ReservationServiceModel model, string userName);
        Task<IEnumerable<ReservationServiceModel>> GetAll();
        Task<IEnumerable<ReservationServiceModel>> GetAllForUser(string userName);
    }
}
