using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.DbContext;

namespace Infrastructure.Services.CarService;

public class CarService(DapperContext context): ICarService
{
    public async Task<Response<List<Car>>> GetCarsAsync()
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = @"select * from cars";
            var result = connection.Query<Car>(sql);
            return new Response<List<Car>>(result.ToList(), "Cars retrieved successfully");
        }
    } 
    public async Task<Response<Car>> GetCarByIdAsync(int id)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = @"select * from cars where id = @id";
            var result = await connection.QueryFirstOrDefaultAsync<Car>(sql, new { Id = id });
            return result == null 
                ? new Response<Car>("Car not found", HttpStatusCode.NotFound)
                : new Response<Car>(result, "Car retrieved successfully"); 
        }
    }

    public async Task<Response<string>> AddCarAsync(Car car)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql =
                @"insert into cars(Model, Manufacturer, Year, PrisePerDay) values(@Model, @Manufacturer, @Year, @PrisePerDay)";
            var result = await connection.ExecuteAsync(sql, car);
            return result == 0 
                ? new Response<string>("Car not added successfully.", HttpStatusCode.InternalServerError) 
                : new Response<string>(null,"Car added successfully");
        }
    }

    public async Task<Response<string>> UpdateCarAsync(Car car)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var cmd = await connection.QueryFirstOrDefaultAsync<Car>("select * from cars where id = @id", car);
            if (cmd == null)
            {
                return new Response<string>("Car not found", HttpStatusCode.NotFound);
            }

            var sql =
                @"update cars set Model = @Model, Manufacturer = @Manufacturer, Year = @Year, PrisePerDay = @PrisePerDay where id = @id";
            var result = await connection.ExecuteAsync(sql, car);
            return result == 0 
                ? new Response<string>("Car not updated successfully", HttpStatusCode.InternalServerError) 
                : new Response<string>(null,"Car updated successfully");
        }
    }

    public async Task<Response<string>> DeleteCarAsync(int id)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var cmd = await connection.QueryFirstOrDefaultAsync<Car>("select * from cars where id = @id", id);
            if (cmd == null)
            {
                return new Response<string>("Car not found", HttpStatusCode.NotFound);
            }
            var sql = "delete from cars where id = @id";
            var result = await connection.ExecuteAsync(sql, cmd);
            return result == 0 
                ? new Response<string>("Car not deleted successfully", HttpStatusCode.NotFound) 
                : new Response<string>(null,"Car deleted successfully");
        }
        
    }
   
}

