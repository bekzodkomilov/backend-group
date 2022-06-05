namespace TaxiDrivers.Models;
public class NewDriver
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public int Age { get; set; } 
    public Guid CarId { get; set; }  
}