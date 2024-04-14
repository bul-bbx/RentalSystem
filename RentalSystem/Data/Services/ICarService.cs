using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Services
{
    public interface ICarService
    {
        Task<IEnumerable<CarServiceModel>> GetAll();
        Task<CarServiceModel> GetByIdAsync(string id);
        Task CreateAsync(CarServiceModel car);
        Task UpdateAsync(CarServiceModel car);
        Task<CarServiceModel> DeleteAsync(string id);
        Task<CarServiceModel> GetCarByBrandAndModel(string brand, string model);
        Task<IEnumerable<CarServiceModel>> GetAvailableCars(DateTime startDate, DateTime endDate);
    }
}
