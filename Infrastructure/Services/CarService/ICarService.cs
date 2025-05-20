using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Services.CarService;

public interface ICarService
{
    Task<Response<List<Car>>> GetCarsAsync();
    Task<Response<Car>> GetCarByIdAsync(int id);
    Task<Response<string>> AddCarAsync(Car car);
    Task<Response<string>> UpdateCarAsync(Car car);
    Task<Response<string>> DeleteCarAsync(int id);
}

