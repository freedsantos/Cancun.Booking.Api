using Cancun.Booking.Domain.Dtos;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Services;

namespace Cancun.Booking.Tests.UnitTests;

public class AvailabilityTest
{
    IAvailabilityService _availabilityService = null!;

    [Fact]
    public void Get_Availability()
    {
        SetUpMocks();

        var availability = _availabilityService.GetAvailabilityAsync(1);

        Assert.NotNull(availability);
        Assert.True(availability.Result.Any());
        Assert.IsAssignableFrom<IEnumerable<AvailabilityDto>>(availability.Result);
    }

    [Fact]
    public void Get_UnavailableDates()
    {
        SetUpMocks();

        var unavailableDates = _availabilityService.GetUnavailableDates(DateTime.Now.Date, DateTime.Now.AddDays(20), 1);

        Assert.NotNull(unavailableDates);
        Assert.True(unavailableDates.Result.Any());
        Assert.IsAssignableFrom<IEnumerable<AvailabilityDto>>(unavailableDates.Result);
    }

    private void SetUpMocks()
    {
        var optionsRules = Mocks.Mocks.GetOptionsMock();
        var repositoryReservationMock = Mocks.Mocks.GetReservationMock();
        var repositoryRoomMock = Mocks.Mocks.GetRoomMock();

        _availabilityService = new AvailabilityService(repositoryReservationMock.Object, repositoryRoomMock.Object, optionsRules.Object);
    }
}