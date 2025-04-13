using Application.GuestManager.Dtos;
using Application.GuestManager.Ports;
using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Application.Shared.Enumns;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("v1/api/guest")]
public class GuestController : ControllerBase
{
    private readonly ILogger<GuestController> _logger;
    private readonly IGuestManagerPort _guestManager;

    public GuestController(IGuestManagerPort guestManager,
                           ILogger<GuestController> logger)
    {
        _logger = logger;
        _guestManager = guestManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(GuestDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(GuestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GuestResponse))]
    public async Task<ActionResult<GuestDto>> CreateAsync([FromBody] GuestDto guest)
    {
        var request = new CreateGuestRequest() { Data = guest };
        var result = await _guestManager.CreateAsync(request);

        if (result.Success)
        {
            return Created("", result.Data);
        }

        if (!result.Success && result.ErrorCode != ErrorCode.GUEST_COULDNOT_STORE_DATA || result.ErrorCode != ErrorCode.GUEST_NOT_FOUND)
        {
            return Conflict(result);
        }
        else if (result.ErrorCode == ErrorCode.GUEST_COULDNOT_STORE_DATA)
        {
            return BadRequest(result);
        }

        _logger.LogError("Response with unknown ErrorCode returned.", result);
        return BadRequest(500);
    }

    [HttpGet()]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GuestDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(GuestResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(GuestResponse))]
    public async Task<ActionResult<GuestDto>> GetAsync([FromQuery] int id)
    {
        var result = await _guestManager.GetAsync(id);

        if (result.Success)
        {
            return Ok(result.Data);
        }

        if (!result.Success && result.ErrorCode == ErrorCode.GUEST_NOT_FOUND)
        {
            return NotFound(result);
        }
        else if (result.ErrorCode == ErrorCode.GUEST_COULDNOT_GET_DATA)
        {
            return BadRequest(result);
        }

        _logger.LogError("Response with unknown ErrorCode returned.", result);
        return BadRequest(500);
    }
}
