using TaxiDrivers.Entities;

namespace TaxiDrivers.Models;
public class GetDriverModel
{
    public GetDriverModel(Driver driver)
    {
        Id = driver.Id;
        Name = $"{driver.FirstName} {driver.LastName}";
        Age = driver.Age;
        PhoneNumber = driver.PhoneNumber;
        NumberOfCars = driver.Cars.Count;
    }
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public int Age { get; set; }
    
    public string PhoneNumber { get; set; }

    public int NumberOfCars { get; set; }
    
}