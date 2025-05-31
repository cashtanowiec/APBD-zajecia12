using System.ComponentModel.DataAnnotations;

namespace APBD_zajecia12.DTO;

public class AddClientDTO
{
    [Required]
    public String FirstName { get; set; }
    [Required]
    public String LastName { get; set; }
    [Required]
    [EmailAddress]
    public String Email { get; set; }
    [Required]
    [Phone]
    public String Telephone { get; set; }
    [Required]
    [Length(11, 11)]
    public String Pesel { get; set; }
    [Required]
    public String TripName { get; set; }
    public DateTime PaymentDate { get; set; }
}