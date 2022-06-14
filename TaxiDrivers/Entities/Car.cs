using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using TaxiDrivers.Entities.Enums;

namespace TaxiDrivers.Entities;
public class Car
{
    [Key]
    public Guid Id { get; set; }
    public string Model { get; set; }

    public string Number { get; set; }
    public string Color { get; set; }
    public ECarType Type { get; set; }

    public ICollection<Driver> Drivers { get; set; }
}