using TaxiDrivers.Entities;
using TaxiDrivers.Entities.Enums;

namespace TaxiDrivers.Models;
public class GetCarModel
{
    public GetCarModel(Car car)
    {
        Id = car.Id;
        Model = car.Model;
        Color = car.Color;
        Number = car.Number;
        Type = car.Type;
        DriverId = car.DriverId;
        DriverName = $"{car.Driver.FirstName} {car.Driver.LastName}";
    }

    public Guid Id { get; set; }
    
    public string Model { get; set; }
    
    public string Number { get; set; }
    
    public string Color { get; set; }

    public Guid DriverId { get; set; }

    public string DriverName { get; set; }
    
    public ECarType Type { get; set; }
}