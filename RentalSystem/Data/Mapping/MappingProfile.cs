using AutoMapper;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Models;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<CarServiceModel, Car>();
            CreateMap<Car, CarServiceModel>();
            CreateMap<Car, CarListingViewModel>();
            CreateMap<CarServiceModel, CarEditViewModel>();
            CreateMap<CarEditViewModel, CarServiceModel>();
            CreateMap<CarServiceModel, CarDeleteViewModel>();
            CreateMap<CarDeleteViewModel, CarServiceModel>();
            CreateMap<CarServiceModel, CarListingViewModel>();
            CreateMap<CarServiceModel, CarDetailsViewModel>();
            CreateMap<CarCreateBindingModel, CarServiceModel>();
            CreateMap<ReservationServiceModel, CarListingViewModel>();

            CreateMap<Reservation, ReservationServiceModel>();
            CreateMap<ReservationServiceModel, Reservation>();
            CreateMap<ReservationServiceModel, ReservationListingViewModel >();
            CreateMap<ReservationCreateBindingModel, ReservationServiceModel>();

            CreateMap<User, UserListingViewModel>();
        }
    }
}
