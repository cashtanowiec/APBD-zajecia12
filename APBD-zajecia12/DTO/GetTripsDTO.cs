namespace APBD_zajecia12.DTO;

public class GetTripsDTO
{
    public int PageNum { get; set; }
    public int PageSize { get; set; }
    public int AllPages { get; set; }
    public List<TripDTO> Trips { get; set; }
}

public class TripDTO
{
    public String Name { get; set; }
    public String Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDTO> Countries { get; set; }
    public List<ClientDTO> Clients { get; set; }
}

public class CountryDTO
{
    public String Name { get; set; }
}

public class ClientDTO
{
    public String FirstName { get; set; }
    public String LastName { get; set; }
}