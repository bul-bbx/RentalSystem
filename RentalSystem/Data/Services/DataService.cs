namespace RentalSystem.Data.Services
{
    public abstract class DataService : BaseService
    {
        protected readonly ApplicationDbContext context;

        protected DataService(ApplicationDbContext context)
        {
            this.context = context;
        }
    }
}
