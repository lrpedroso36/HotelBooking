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


    [Fact]
    public void ShouldStateStatusToCancelWhenCancelingABookingWithCreatedStatus()
    {
        //Arrange
        //Act
        _booking.ChangeState(Domain.Enums.Action.Cancel);
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Canceled);
    }

    [Fact]
    public void ShouldSetStatusToFinishedWhenFinishingAPaidBooking()
    {
        //Arrange
        //Act
        _booking.ChangeState(Domain.Enums.Action.Pay);
        _booking.ChangeState(Domain.Enums.Action.Finish);
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Finished);
    }

    [Fact]
    public void ShouldSetStatusToRefoundedWhenRefaoundingAPaidBooking()
    {
        //Arrange
        //Act
        _booking.ChangeState(Domain.Enums.Action.Pay);
        _booking.ChangeState(Domain.Enums.Action.Refound);
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Refounded);
    }

    [Fact]
    public void ShouldSetStatusToCreatedWhenReopeningACancelBooking()
    {
        //Arrange
        //Act
        _booking.ChangeState(Domain.Enums.Action.Cancel);
        _booking.ChangeState(Domain.Enums.Action.Reopen);
        var currentStatus = _booking.CurrentStatus;

        //Assert
        currentStatus.Should().Be(Status.Created);
    }
}
