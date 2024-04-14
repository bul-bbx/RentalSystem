using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RentalSystem.Data;
using RentalSystem.Data.Entities;
using RentalSystem.Data.Mapping;
using RentalSystem.Data.Middleware;
using RentalSystem.Data.Services;


namespace RentalSystem
{
    public class StartUp
    {
        // Constructor
        public StartUp(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /* This method gets called by the runtime. 
           Use this method to add services to the container. */
        public void ConfigureServices(IServiceCollection services)
        {
            // Add DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Add AutoMapper
            services.AddAutoMapper(typeof(StartUp)); // Assuming AutoMapper configurations are in the same assembly

            // Add Identity with custom user and role types
            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            })
            .AddDefaultUI()
            .AddRoles<IdentityRole>()
            .AddDefaultTokenProviders()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            // Add UserManager<User>
            services.AddScoped<UserManager<User>>();

            // Add transient services
            services.AddTransient<ICarService, CarService>();
            services.AddTransient<IReservationService, ReservationService>();

            // Ensure roles are seeded
            services.AddTransient<RoleSeeder>();

            // Add logging service
            services.AddLogging();

            // Add controllers and Razor Pages
            services.AddControllersWithViews();
            services.AddRazorPages();
        }

        /* This method gets called by the runtime. 
           Use this method to configure the HTTP request pipeline. */
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, RoleSeeder roleSeeder, ApplicationDbContext context)
        {
            if (env.IsDevelopment())
            {
                context.Database.EnsureCreated();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            // Seed roles
            roleSeeder.SeedRolesAsync(app.ApplicationServices).Wait();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}