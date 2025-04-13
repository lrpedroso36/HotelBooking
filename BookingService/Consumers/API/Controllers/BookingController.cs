using Application.BookingManager.Dtos;
using Application.BookingManager.Ports;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("v1/api/booking")]
public class BookingController : ControllerBase
{
    private readonly IBookingManagerPort _bookingManager;

    public BookingController(IBookingManagerPort bookingManager)
    {
        _bookingManager = bookingManager;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] BookingDto booking)
    {

    }

}
