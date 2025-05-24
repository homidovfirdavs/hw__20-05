using Microsoft.AspNetCore.Http;

namespace Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string Model { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal PricePerDay { get; set; }
    public IFormFile Photo { get; set; }
}
