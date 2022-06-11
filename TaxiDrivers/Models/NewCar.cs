using TaxiDrivers.Entities.Enums;

namespace TaxiDrivers.Models;
public class NewCar
{
    public string Model { get; set; }
    public string Number { get; set; }
    public string Color { get; set; }
    public Guid DriverId { get; set; }
    public ECarType Type { get; set; }
}