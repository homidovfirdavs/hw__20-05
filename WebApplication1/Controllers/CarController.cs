using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Services.CarService;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class CarController(ICarService _carService) : Controller
{
   
    [HttpGet("getall")]
    public Task<Response<List<Car>>> GetCarsAsync()
    {
        var result = _carService.GetCarsAsync();
        return result;
    }
    
    [HttpGet("getbyid/{id}")]
    public Task<Response<Car>> GetCarByIdAsync(int id)
    {
        var result = _carService.GetCarByIdAsync(id);
        return result;
    }
    
    [HttpPost("addcar")]
    public Task<Response<string>> AddCarAsync(Car car)
    {
        var result = _carService.AddCarAsync(car);
        return result;
    }
    
    [HttpPut("updatecar")]
    Task<Response<string>> UpdateCarAsync(Car car)
    {
        var result = _carService.UpdateCarAsync(car);
        return result;
    }
    
    [HttpDelete("deletecar/{id}")]
    public Task<Response<string>> DeleteCarAsync(int id)
    {
        var result = _carService.DeleteCarAsync(id);
        return result;
    }
   
}