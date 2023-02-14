using AutoMapper;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Profiles;
using Cancun.Booking.Repository.Seed;
using Microsoft.Extensions.Options;
using Moq;
using System.Linq.Expressions;

namespace Cancun.Booking.Tests.Mocks
{
    public static class Mocks
    {
        public static Mock<IRepository<Reservation>> GetReservationMock()
        {
            var repositoryReservationMock = new Mock<IRepository<Reservation>>();
            repositoryReservationMock!.Setup(x => x.GetAllAsync())
                                      .ReturnsAsync(ReservationSeed.Seed().ToList());
            repositoryReservationMock!.Setup(x => x.GetAllByAsync(It.IsAny<Expression<Func<Reservation, bool>>>()))
                                      .ReturnsAsync(ReservationSeed.Seed().ToList());
            repositoryReservationMock!.Setup(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Reservation, bool>>>()))
                                      .ReturnsAsync(ReservationSeed.Seed().FirstOrDefault());
            repositoryReservationMock!.Setup(x => x.AnyAsync(m => m.Id == 1))
                                      .ReturnsAsync(ReservationSeed.Seed().Any(m => m.Id == 1));
            return repositoryReservationMock;
        }

        public static Mock<IOptions<BookingRules>> GetOptionsMock()
        {
            var optionsRules = new Mock<IOptions<BookingRules>>();
            var option = new BookingRules { ReservationMaxDays = 3, ReservationMaxDaysAhead = 30, ReservationMinDaysAhead = 1 };
            optionsRules!.Setup(x => x.Value).Returns(option);
            return optionsRules;
        }

        public static Mock<IRepository<User>> GetUserMock()
        {
            var repositoryUserMock = new Mock<IRepository<User>>();
            repositoryUserMock!.Setup(x => x.GetAllAsync())
                               .ReturnsAsync(UserSeed.Seed().ToList());
            repositoryUserMock!.Setup(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<User, bool>>>()))
                               .ReturnsAsync(UserSeed.Seed().FirstOrDefault());
            repositoryUserMock!.Setup(x => x.AnyAsync(m => m.Id == 1))
                               .ReturnsAsync(UserSeed.Seed().Any(m => m.Id == 1));
            return repositoryUserMock;
        }

        public static Mock<IRepository<Room>> GetRoomMock()
        {
            var repositoryRoomMock = new Mock<IRepository<Room>>();
            repositoryRoomMock!.Setup(x => x.GetAllAsync())
                               .ReturnsAsync(RoomSeed.Seed().ToList());
            repositoryRoomMock!.Setup(x => x.GetFirstOrDefaultAsync(It.IsAny<Expression<Func<Room, bool>>>()))
                               .ReturnsAsync(RoomSeed.Seed().FirstOrDefault());
            repositoryRoomMock!.Setup(x => x.AnyAsync(m => m.Id == 1))
                               .ReturnsAsync(RoomSeed.Seed().Any(m => m.Id == 1));
            return repositoryRoomMock;
        }

        public static IMapper MapperMock()
        {
            var mapperMock = new MapperConfiguration(cfg => { cfg.AddProfile(new ReservationProfile()); });
            var mapper = mapperMock.CreateMapper();
            return mapper;
        }
    }
}
