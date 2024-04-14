using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Models.Entities;
using RentalSystem.Data;

namespace RentalSystem.Data.Services
{
    public class CarService : DataService, ICarService
    {
        private readonly IMapper _mapper;

        public CarService(ApplicationDbContext context, IMapper mapper): base(context)
        {
            _mapper = mapper;
        }

        public async Task CreateAsync(CarServiceModel model)
        {
            if (!IsEntityStateValid(model))
            {
                throw new ArgumentException("Invalid entity state.");
            }

            var carEntity = _mapper.Map<Car>(model);

            await context.Cars.AddAsync(carEntity);
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CarServiceModel>> GetAll()
        {
            var cars = await context.Cars
                .Where(e => e.PricePerDay > 0)
                .ProjectTo<CarServiceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return cars;
        }

        public async Task<CarServiceModel> GetByIdAsync(string id)
        {
            var carEntity = await context.Cars.FindAsync(id);

            return _mapper.Map<CarServiceModel>(carEntity);
        }

        public async Task<CarServiceModel> GetCarByBrandAndModel(string brand, string model)
        {
            var carEntity = await context.Cars
                .FirstOrDefaultAsync(c => c.Brand == brand && c.Model == model);

            return _mapper.Map<CarServiceModel>(carEntity);
        }

        public async Task UpdateAsync(CarServiceModel model)
        {
            if (!IsEntityStateValid(model))
            {
                throw new ArgumentException("Invalid entity state.");
            }

            var existingCarEntity = await context.Cars.FindAsync(model.Id);
            if (existingCarEntity == null)
            {
                throw new InvalidOperationException("Car not found.");
            }

            _mapper.Map(model, existingCarEntity);

            await context.SaveChangesAsync();
        }

        public async Task<CarServiceModel> DeleteAsync(string id)
        {
            var carEntity = await context.Cars.FindAsync(id);
            if (carEntity != null)
            {
                var deletedCarModel = _mapper.Map<CarServiceModel>(carEntity); // Map the deleted car entity to CarServiceModel

                context.Cars.Remove(carEntity);
                await context.SaveChangesAsync();

                return deletedCarModel; // Return the deleted car
            }
            return null; // Return null if the car was not found
        }

        public async Task<IEnumerable<CarServiceModel>> GetAvailableCars(DateTime startDate, DateTime endDate)
        {
            // Find cars that are not already rented for the specified date range
            var availableCars = await context.Cars
                .Where(car => !context.Reservations
                    .Any(request => request.CarId == car.Id &&
                                    (startDate <= request.EndDate && endDate >= request.StartDate)))
                .ProjectTo<CarServiceModel>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return availableCars;
        }
    }
}
