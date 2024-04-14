using AutoMapper;
using RentalSystem.Data.Mapping;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Models
{
    public class ReservationListingViewModel : IHaveCustomMapping
    {
        public CarListingViewModel Car { get; set; }

        public UserListingViewModel User { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public void ConfigureMapping(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<ReservationServiceModel, ReservationListingViewModel>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
                .ForMember(dest => dest.Car, opt => opt.MapFrom(src => src.Car));
        }
    }
}
