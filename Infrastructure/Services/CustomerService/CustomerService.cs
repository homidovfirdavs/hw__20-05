using System.Net;
using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.DbContext;

namespace Infrastructure.Services.CustomerService;

public class CustomerService(DapperContext context) : ICustomerService
{
   public async Task<Response<List<Customer>>> GetCustomersAsync()
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = "select * from customers";
            var result = await connection.QueryAsync<Customer>(sql);
            return new Response<List<Customer>>(result.ToList(), "Customers retrieved successfully");
        }
    }

    public async Task<Response<Customer>> GetCustomerByIdAsync(int id)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = "select * from customer where id=@Id";
            var result = await connection.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
            return result == null 
                ? new Response<Customer>("Customer not found", HttpStatusCode.NotFound) 
                : new Response<Customer>(result, "Customer retrieved successfully");
        }
    }

    public async Task<Response<string>> AddCustomerAsync(Customer customer)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var sql = @"insert into customer(FullName, Phone, Email) values (@FullName, @Phone, @Email)";
            var result = await connection.ExecuteAsync(sql, customer);
            return result == 0 
                ? new Response<string>("Customer not added successfully", HttpStatusCode.InternalServerError) 
                : new Response<string>(null,"Customer added successfully");
        }
    }

    public async Task<Response<string>> UpdateCustomerAsync(Customer customer)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var cmd = await connection.QueryFirstOrDefaultAsync<Customer>("select * from customers where id = @id", customer);
            if (cmd == null)
            {
                return new Response<string>("Customer not found", HttpStatusCode.NotFound);
            }

            var sql =
                @"update customers set FullName = @FullName, Phone = @Phone, Email = @Email  where id = @id";
            var result = await connection.ExecuteAsync(sql, customer);
            return result == 0 
                ? new Response<string>("Customer not updated successfully", HttpStatusCode.InternalServerError) 
                : new Response<string>(null,"Customer updated successfully");
        }
    }

    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        using (var connection = await context.GetConnectionAsync())
        {
            var cmd = await connection.QueryFirstOrDefaultAsync<Customer>("select * from customers where id = @id", id);
            if (cmd == null)
            {
                return new Response<string>("Customer not found", HttpStatusCode.NotFound);
            }
            var sql = "delete from customers where id = @id";
            var result = await connection.ExecuteAsync(sql, cmd);
            return result == 0 
                ? new Response<string>("Customer not deleted successfully", HttpStatusCode.NotFound) 
                : new Response<string>(null,"Customer deleted successfully");
        }
    }
}