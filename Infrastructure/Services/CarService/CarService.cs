using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.DbContext;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;

namespace Infrastructure.Services.CarService;

public class CarService(DapperContext context, IWebHostEnvironment webHostEnvironment): ICarService
{
    public async Task<Response<List<Car>>> GetCarsAsync()
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = @"select * from cars";
            var result =  connection.Query<Car>(sql);
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
        
        var wwwRootPath = webHostEnvironment.WebRootPath;
        var folderPath = Path.Combine(wwwRootPath, "CarPhoto");
        var fileName = car.Photo.FileName;
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        var fullPath = Path.Combine(folderPath, fileName);
        await using (var connection = await context.GetConnectionAsync())
        {

            await using (var stream = File.Create(fullPath))
            {
                await car.Photo.CopyToAsync(stream);
            }


            var sql =
                @"insert into cars(Model, Manufacturer, Year, PricePerDay, Photo) values(@Model, @Manufacturer, @Year, @PricePerDay, @Photo)";
            var anonymousObject = new
            {
                Model = car.Model,
                Manufacturer = car.Manufacturer,
                Year = car.Year,
                PricePerDay = car.PricePerDay,
                Photo = car.Photo.FileName
            };
            var result = await connection.ExecuteAsync(sql, anonymousObject);
            return result == 0
                ? new Response<string>("Car not added successfully.", HttpStatusCode.InternalServerError)
                : new Response<string>(null, "Car added successfully");
        }

    }

    public async Task<Response<string>> UpdateCarAsync(Car car)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var cmd = await connection.QueryFirstOrDefaultAsync<Car>("select * from cars where id = @id", new {id = car.Id});
            if (cmd == null)
            {
                return new Response<string>("Car not found", HttpStatusCode.NotFound);
            }

            var sql =
                @"update cars set Model = @Model, Manufacturer = @Manufacturer, Year = @Year, PricePerDay = @PricePerDay where id = @id";
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

