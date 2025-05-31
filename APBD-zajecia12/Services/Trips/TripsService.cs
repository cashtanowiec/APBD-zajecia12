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

        var trips = await _databaseContext.Trips.Include(t => t.IdCountries).Include(t => t.ClientTrips).ThenInclude(ct => ct.IdClientNavigation).Skip((page-1) * pageSize).OrderByDescending(trip => trip.DateFrom).Select(trip => new TripDTO()
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
                .FirstOrDefaultAsync(c => c.Pesel == addClientDto.Pesel);

            if (client != null)
                throw new ArgumentException("Client already exists!");

            // 2.
            if (await _databaseContext.ClientTrips
                    .AnyAsync(ct => ct.IdClientNavigation.Pesel == addClientDto.Pesel && ct.IdTrip == idTrip))
            {
                throw new ArgumentException("Client with the same PESEL is already assigned to the trip!");
            }

            // 3.
            if (! await _databaseContext.Trips.AnyAsync(trip => trip.IdTrip == idTrip && trip.DateFrom > DateTime.Now))
            {
                throw new ArgumentException("Trip doesn't exist or it has already begun!");
            }
            
            // 4.
            client = new Models.Client()
            {
                FirstName = addClientDto.FirstName,
                LastName = addClientDto.LastName,
                Email = addClientDto.Email,
                Pesel = addClientDto.Pesel,
                Telephone = addClientDto.Telephone
            };
            await _databaseContext.Clients.AddAsync(client);
            await _databaseContext.SaveChangesAsync();

            var clientTrip = new ClientTrip()
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                RegisteredAt = DateTime.Now,
                PaymentDate = addClientDto.PaymentDate
            };
            await _databaseContext.ClientTrips.AddAsync(clientTrip);
            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();

            return client.IdClient;

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}