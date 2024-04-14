using AutoMapper;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Mapping;
using RentalSystem.Data.Models.Entities;

namespace RentalSystem.Data.Models
{
    public class UserListingViewModel : IHaveCustomMapping
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string EGN { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsAdmin { get; set; }

        public void ConfigureMapping(IMapperConfigurationExpression mapper)
        {
            mapper.CreateMap<User, UserListingViewModel>().ForMember(a => a.Id, b => b.MapFrom(c => c.Id));
            mapper.CreateMap<User, UserListingViewModel>().ForMember(a => a.Email, b => b.MapFrom(c => c.Email));
            mapper.CreateMap<UserServiceModel, UserListingViewModel>().ForMember(a => a.UserName, b => b.MapFrom(c => c.UserName));
        }
    }
}
