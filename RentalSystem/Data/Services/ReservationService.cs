using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Services
{
    public class ReservationService : DataService, IReservationService
    {
        private readonly IMapper _mapper;

        public ReservationService(ApplicationDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<bool> Create(ReservationServiceModel model, string userName)
        {
            if (!this.IsEntityStateValid(model))
            {
                return false;
            }

            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            var car = await this.context.Cars.SingleOrDefaultAsync(c => c.Id == model.CarId);
            if (user == null || car == null)
            {
                return false;
            }

            var request = _mapper.Map<Reservation>(model);
            request.User = user;

            this.context.Cars.Update(car);
            await this.context.Reservations.AddAsync(request);
            await this.context.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ReservationServiceModel>> GetAll()
        {
            var requests = await this.context.Reservations
                .ProjectTo<ReservationServiceModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return requests;
        }

        public async Task<IEnumerable<ReservationServiceModel>> GetAllForUser(string userName)
        {
            var user = await this.context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
            if (user == null)
            {
                return null;
            }

            var requests = await this.context.Reservations
                .Where(r => r.UserId == user.Id)
                .ProjectTo<ReservationServiceModel>(_mapper.ConfigurationProvider)
                .ToArrayAsync();

            return requests;
        }

        public async Task<ReservationServiceModel> AddRequestAsync(ReservationServiceModel requestModel)
        {
            if (!this.IsEntityStateValid(requestModel))
            {
                return null;
            }

            // Map request model to entity
            var request = _mapper.Map<Reservation>(requestModel);

            // Add request to context and save changes
            await this.context.Reservations.AddAsync(request);
            await this.context.SaveChangesAsync();

            // Map entity back to service model
            var createdRequest = _mapper.Map<ReservationServiceModel>(request);

            return createdRequest;
        }
    }
}
