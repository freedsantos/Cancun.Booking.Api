using Cancun.Booking.Domain.Entities;
using Cancun.Booking.Domain.Interfaces;
using Cancun.Booking.Domain.Services;
using Moq;

namespace Cancun.Booking.Tests.UnitTests;

public class UserTest
{
    IUserService _userService = null!;

    [Fact]
    public void GetAll_User()
    {
        SetUpMocks();

        var users = _userService.GetAll();

        Assert.NotNull(users);
        Assert.True(users.Result.Any());
        Assert.IsAssignableFrom<IEnumerable<User>>(users.Result);
    }

    [Fact]
    public void Get_User()
    {
        SetUpMocks();

        var user = _userService.Get(1);

        Assert.NotNull(user);
        Assert.IsType<User>(user.Result);
        Assert.Equal(1, user.Result.Id);
        Assert.Equal("Fred Santos", user.Result.Name);
    }

    private void SetUpMocks()
    {
        Mock<IRepository<User>> repositoryUserMock = Mocks.Mocks.GetUserMock();
        _userService = new UserService(repositoryUserMock.Object);
    }
}