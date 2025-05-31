using APBD_zajecia12.DTO;
using APBD_zajecia12.Services.Trips;
using Microsoft.AspNetCore.Mvc;
namespace APBD_zajecia12.Controllers;
    

[Route("api/{controller}")]
[ApiController]
public class TripsController : ControllerBase
{
    private readonly ITripsService _tripsService;

    public TripsController(ITripsService tripsService)
    {
        _tripsService = tripsService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetTrips(int page = 1, int pageSize = 10)
    {
        try
        {
            var data = await _tripsService.GetTrips(page, pageSize);
            return Ok(data);
        }
        catch (Exception exc)
        {
            return NotFound(exc.Message);
        }
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClient(int idTrip, AddClientDTO addClientDto)
    {
        try
        {
            var id = await _tripsService.AddClient(idTrip, addClientDto);
            return Ok(id);
        }
        catch (Exception exc)
        {
            return BadRequest(exc.Message);
        }
    }
    

}

