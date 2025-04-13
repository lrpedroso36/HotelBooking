using Application.BookingManager.Dtos;
using Application.BookingManager.Ports;
using Application.BookingManager.Responses;
using Application.BookingManager.Resquets;
using Application.Shared.Enumns;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("v1/api/booking")]
public class BookingController : ControllerBase
{
    private readonly ILogger<BookingController> _logger;
    private readonly IBookingManagerPort _bookingManager;

    public BookingController(ILogger<BookingController> logger,
                             IBookingManagerPort bookingManager)
    {
        _logger = logger;
        _bookingManager = bookingManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BookingDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(BookingResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BookingResponse))]
    public async Task<ActionResult> PostAsync([FromBody] BookingDto booking)
    {
        var request = new CreateBookingResquest() { Data = booking };
        var result = await _bookingManager.CreateAsync(request);

        if (result.Success)
        {
            return Created("", result.Data);
        }

        if (!result.Success && result.ErrorCode != ErrorCode.BOOKING_COULDNOT_STORE_DATA || result.ErrorCode != ErrorCode.BOOKING_NOT_FOUND)
        {
            return Conflict(result);
        }
        else if (result.ErrorCode == ErrorCode.BOOKING_COULDNOT_STORE_DATA)
        {
            return BadRequest(result);
        }

        _logger.LogError("Response with unknown ErrorCode returned.", result);
        return BadRequest(500);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BookingDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(BookingResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(BookingResponse))]
    public async Task<ActionResult<BookingDto>> GetAsync([FromQuery] int id)
    {
        var result = await _bookingManager.GetAsync(id);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        if (!result.Success && result.ErrorCode == ErrorCode.BOOKING_NOT_FOUND)
        {
            return NotFound(result);
        }
        else if (result.ErrorCode == ErrorCode.BOOKING_COULDNOT_GET_DATA)
        {
            return BadRequest(result);
        }

        _logger.LogError("Response with unknown ErrorCode returned.", result);
        return BadRequest(500);
    }

}
