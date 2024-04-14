using AutoMapper;

namespace RentalSystem.Data.Mapping
{
    public interface IHaveCustomMapping
    {
        void ConfigureMapping(IMapperConfigurationExpression mapper);
    }
}
