using AutoMapper;
using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Resources.Handlers;
using Cancun.Booking.Domain.Resources.Helpers;
using Cancun.Booking.Domain.Resources.Validators;
using Cancun.Booking.Domain.Services;
using Microsoft.Extensions.Options;
using Moq;

namespace Cancun.Booking.Tests.UnitTests;

public class ReservationModifyTest
{
    IReservationService _reservationService = null!;
    BookingRules _options = null!;

    [Fact]
    public void Modify_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(26),
            EndDate = DateTime.Now.AddDays(28)
        };

        var updateResult = _reservationService.Update(dto);

        Assert.NotNull(updateResult);
        Assert.IsType<Reservation>(updateResult.Result);
        Assert.Equal(dto.Id, updateResult.Result.Id);
        Assert.Equal(dto.UserId, updateResult.Result.UserId);
        Assert.Equal(dto.RoomId, updateResult.Result.RoomId);
        Assert.Equal(dto.StartDate.Date, updateResult.Result.StartDate);
        Assert.Equal(dto.EndDate.Date, updateResult.Result.EndDate);
    }


    [Fact]
    public void MaxDuration_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(26),
            EndDate = DateTime.Now.AddDays(29)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Contains(string.Format(MessagesHelper.ReservationMaxDays, _options.ReservationMaxDays), ex.Result.Message);
    }

    [Fact]
    public void MinDate_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now,
            EndDate = DateTime.Now.AddDays(2)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Equal(MessagesHelper.MinReservationDate, ex.Result.Message);
    }

    [Fact]
    public void MaxDate_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(29),
            EndDate = DateTime.Now.AddDays(31)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Equal(string.Format(MessagesHelper.MaxReservationDate, _options.ReservationMaxDaysAhead), ex.Result.Message);
    }

    [Fact]
    public void EndGreaterThanStart_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(29),
            EndDate = DateTime.Now.AddDays(26)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Equal(MessagesHelper.EndDateGreaterThanStart, ex.Result.Message);
    }

    [Fact]
    public void NotAvailable_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(12),
            EndDate = DateTime.Now.AddDays(14)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Contains(string.Format(MessagesHelper.DatesNotAvailable, string.Empty), ex.Result.Message);
    }

    [Fact]
    public void UserNotFound_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 99,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(3),
            EndDate = DateTime.Now.AddDays(5)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Contains(string.Format(MessagesHelper.NotFound, string.Empty), ex.Result.Message);
    }

    [Fact]
    public void RoomNotFound_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 1,
            UserId = 1,
            RoomId = 99,
            StartDate = DateTime.Now.AddDays(3),
            EndDate = DateTime.Now.AddDays(5)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Contains(string.Format(MessagesHelper.NotFound, string.Empty), ex.Result.Message);
    }

    [Fact]
    public void NotValid_Reservation()
    {
        SetUpMocks();

        ReservationUpdateDto dto = new ReservationUpdateDto
        {
            Id = 99,
            UserId = 1,
            RoomId = 1,
            StartDate = DateTime.Now.AddDays(3),
            EndDate = DateTime.Now.AddDays(5)
        };

        var ex = Assert.ThrowsAsync<UserFriendlyException>(() => _reservationService.Update(dto));
        Assert.Contains(string.Format(MessagesHelper.NotFound, string.Empty), ex.Result.Message);
    }

    private void SetUpMocks()
    {
        Mock<IRepository<Reservation>> repositoryReservationMock = Mocks.Mocks.GetReservationMock();
        Mock<IRepository<Room>> repositoryRoomMock = Mocks.Mocks.GetRoomMock();
        Mock<IRepository<User>> repositoryUserMock = Mocks.Mocks.GetUserMock();
        Mock<IOptions<BookingRules>> optionsRules = Mocks.Mocks.GetOptionsMock();

        var availabilityService = new AvailabilityService(repositoryReservationMock.Object, repositoryRoomMock.Object, optionsRules.Object);
        var createValidatorMock = new ReservationCreateValidator(repositoryRoomMock.Object, repositoryUserMock.Object, availabilityService, optionsRules.Object);
        var updateValidatorMock = new ReservationUpdateValidator(repositoryRoomMock.Object, repositoryUserMock.Object, repositoryReservationMock.Object, availabilityService, optionsRules.Object);

        IMapper mapper = Mocks.Mocks.MapperMock();

        _reservationService = new ReservationService(repositoryReservationMock.Object, createValidatorMock, updateValidatorMock, mapper);
        _options = optionsRules.Object.Value;
    }
}