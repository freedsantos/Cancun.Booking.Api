using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Handlers;
using Cancun.Booking.Domain.Resources.Helpers;
using Cancun.Booking.Domain.Resources.Validators;
using Cancun.Booking.Domain.Services;
using Cancun.Booking.Repository.Context;
using Cancun.Booking.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cancun.Booking.Api
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddDbContext<BookingContext>(opt => opt.UseInMemoryDatabase("BookingDatabase"));

            DependencyInjection(builder);

            builder.Services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            SetDevelopmentEnv(app);
            SeedData(app);

            app.UseMiddleware<ErrorHandler>();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        private static void SetDevelopmentEnv(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
        }

        private static void SeedData(WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var seedService = scope.ServiceProvider.GetService<ISeedService>();
                seedService!.SeedAsync();
            }
        }

        private static void DependencyInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ISeedService, SeedService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRoomService, RoomService>();
            builder.Services.AddScoped<IReservationService, ReservationService>();
            builder.Services.AddScoped<IAvailabilityService, AvailabilityService>();

            builder.Services.AddScoped<IRepository<User>, BaseRepository<BookingContext, User>>();
            builder.Services.AddScoped<IRepository<Room>, BaseRepository<BookingContext, Room>>();
            builder.Services.AddScoped<IRepository<Reservation>, BaseRepository<BookingContext, Reservation>>();

            builder.Services.AddScoped<ISeedRepository, SeedRepository>();

            builder.Services.AddScoped<ReservationCreateValidator>();
            builder.Services.AddScoped<ReservationUpdateValidator>();

            builder.Services.AddOptions();
            builder.Services.Configure<BookingRules>(builder.Configuration.GetSection("BookingRules"));
        }
    }
}