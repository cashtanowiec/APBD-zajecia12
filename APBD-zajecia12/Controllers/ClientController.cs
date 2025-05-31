using APBD_zajecia12.Services.Client;
using Microsoft.AspNetCore.Mvc;

namespace APBD_zajecia12.Controllers;

[Route("api/{controller}")]
[ApiController]
public class ClientController : ControllerBase
{
    private readonly IClientService _clientService;
    public ClientController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Remove(int id)
    {
        try
        {
            await _clientService.Remove(id);
            return NoContent();
        }
        catch (KeyNotFoundException exc)
        {
            return NotFound(exc.Message);
        }
        catch (InvalidOperationException exc)
        {
            return Conflict(exc.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}