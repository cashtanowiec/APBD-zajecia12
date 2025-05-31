using APBD_zajecia12.DTO;

namespace APBD_zajecia12.Services.Trips;

public interface ITripsService
{
    Task<GetTripsDTO> GetTrips(int page, int pageSize);
    Task<int> AddClient(int idTrip, AddClientDTO addClientDto);
}