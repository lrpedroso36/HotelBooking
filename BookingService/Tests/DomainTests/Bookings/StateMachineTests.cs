using Domain.Entities;
using Domain.Enums;
using FluentAssertions;

namespace DomainTests.Bookings;

public class StateMachineTests
{
    private readonly Booking _booking;

    public StateMachineTests()
    {
        _booking = new Booking();
    }

    [Fact]
    public void ShouldAwaysStartWithCreatedStatus()
    {
        //Arrange
        //Act
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Created);
    }

    [Fact]
    public void ShouldStateStatusToPaidWhenPayingForBookingWithCreatedStatus()
    {
        //Arrange
        //Act
        _booking.ChangeState(Domain.Enums.Action.Pay);
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Paid);
    }
}
