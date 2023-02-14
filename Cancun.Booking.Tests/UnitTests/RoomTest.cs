using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Services;
using Moq;

namespace Cancun.Booking.Tests.UnitTests;

public class RoomTest
{
    IRoomService _roomService = null!;

    [Fact]
    public void GetAll_Room()
    {
        SetUpMocks();

        var rooms = _roomService.GetAll();

        Assert.NotNull(rooms);
        Assert.True(rooms.Result.Any());
        Assert.IsAssignableFrom<IEnumerable<Room>>(rooms.Result);
    }

    [Fact]
    public void Get_Room()
    {
        SetUpMocks();

        var room = _roomService.Get(1);

        Assert.NotNull(room);
        Assert.IsType<Room>(room.Result);
        Assert.Equal(1, room.Result.Id);
        Assert.Equal("My Only Room", room.Result.Name);
    }

    private void SetUpMocks()
    {
        Mock<IRepository<Room>> repositoryRoomMock = Mocks.Mocks.GetRoomMock();
        _roomService = new RoomService(repositoryRoomMock.Object);
    }
}