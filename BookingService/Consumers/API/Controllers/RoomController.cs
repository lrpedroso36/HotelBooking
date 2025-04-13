using Application.GuestManager.Responses;
using Application.GuestManager.Resquets;
using Application.RoomManager.Dtos;
using Application.RoomManager.Ports;
using Application.Shared.Enumns;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[Route("v1/api/room")]
public class RoomController : ControllerBase
{
    private readonly ILogger<RoomController> _logger;
    private readonly IRoomManagerPort _roomManager;

    public RoomController(IRoomManagerPort roomManager,
                          ILogger<RoomController> logger)
    {
        _logger = logger;
        _roomManager = roomManager;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(RoomDto))]
    [ProducesResponseType(StatusCodes.Status409Conflict, Type = typeof(RoomResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RoomResponse))]
    public async Task<ActionResult<RoomDto>> CreateAsync([FromBody] RoomDto room)
    {
        var request = new CreateRoomRequest() { Data = room };
        var result = await _roomManager.CreateAsync(request);

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
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoomDto))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(RoomResponse))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(RoomResponse))]
    public async Task<ActionResult<RoomDto>> GetAsync([FromQuery] int id)
    {
        var result = await _roomManager.GetAsync(id);

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

