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
    public async Task<Response<List<Car>>> GetCarsAsync()
    {
        var result = await _carService.GetCarsAsync();
        return result;
    }
    
    [HttpGet("getbyid/{id}")]
    public async Task<Response<Car>> GetCarByIdAsync(int id)
    {
        var result = await _carService.GetCarByIdAsync(id);
        return result;
    }
    
    [HttpPost("addcar")]
    public async Task<Response<string>> AddCarAsync([FromForm] Car car)
    {
        var result = await _carService.AddCarAsync(car);
        return result;
    }
    
    [HttpPut("updatecar")]
    public async Task<Response<string>> UpdateCarAsync(Car car)
    {
        var result = await _carService.UpdateCarAsync(car);
        return result;
    }
    
    [HttpDelete("deletecar/{id}")]
    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        var result = await _carService.DeleteCarAsync(id);
        return result;
    }
   
}