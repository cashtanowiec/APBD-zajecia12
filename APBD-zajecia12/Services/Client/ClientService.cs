using APBD_zajecia12.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace APBD_zajecia12.Services.Client;

public class ClientService : IClientService
{
    private readonly _2019sbdContext _databaseContext;
    public ClientService(_2019sbdContext databaseContext)
    {
        _databaseContext = databaseContext;
    }
    
    public async Task Remove(int id)
    {
        await using var transaction = await _databaseContext.Database.BeginTransactionAsync();

        try
        {
            var client = await _databaseContext.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.IdClient == id);
            
            if (client == null)
                throw new ArgumentException("Client doesn't exist!");

            if (client.ClientTrips.Any())
                throw new ArgumentException("Client has assigned trips!");

            _databaseContext.Clients.Remove(client);
            await _databaseContext.SaveChangesAsync();
            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
        
    }
}