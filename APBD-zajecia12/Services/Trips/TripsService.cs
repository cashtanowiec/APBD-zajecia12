using APBD_zajecia12.DTO;
using APBD_zajecia12.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_zajecia12.Services.Trips;

public class TripsService : ITripsService
{
    private readonly _2019sbdContext _databaseContext;
    public TripsService(_2019sbdContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task<GetTripsDTO> GetTrips(int page, int pageSize)
    {

        var totalCount = await _databaseContext.Trips.CountAsync();
        var dto = new GetTripsDTO()
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = (int) Math.Ceiling(1.0 * totalCount / pageSize)
        };

        var trips = await _databaseContext.Trips.Skip((page-1) * pageSize).OrderByDescending(trip => trip.DateFrom).Select(trip => new TripDTO()
        {
            Name = trip.Name,
            Description = trip.Description,
            DateFrom = trip.DateFrom,
            DateTo = trip.DateTo,
            MaxPeople = trip.MaxPeople,
            Countries = trip.IdCountries.Select(country => new CountryDTO()
            {
                Name = country.Name
            }).ToList(),
            Clients = trip.ClientTrips.Select(clientTrips => new ClientDTO
            {
                FirstName = clientTrips.IdClientNavigation.FirstName,
                LastName = clientTrips.IdClientNavigation.LastName
            }).ToList()
        }).ToListAsync();

        if (trips == null)
        {
            throw new ArgumentException("Trips not found!");
        }
        
        dto.Trips = trips;
        return dto;
    }

    public async Task<int> AddClient(int idTrip, AddClientDTO addClientDto)
    {
        await using var transaction = await _databaseContext.Database.BeginTransactionAsync();
        try
        {
            // 1.
            var client = await _databaseContext.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.Pesel == addClientDto.Pesel);

            if (client != null)
                throw new ArgumentException("Client already exists!");

            // 2.
            if (_databaseContext.ClientTrips.Any(c => c.IdTrip == idTrip))
            {
                throw new ArgumentException("Client is already assigned to the trip!");
            }

            // 3.
            if (! _databaseContext.Trips.Any(trip => trip.IdTrip == idTrip && trip.DateFrom > DateTime.Now))
            {
                throw new ArgumentException("Trip doesn't exist or it has already begun!");
            }
            
            // 4.
            //await _databaseContext.Clients.AddAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}