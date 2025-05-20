using Domain.Entities;
using Infrastructure.Services.RentalService;
using Npgsql;

namespace Infrastructure.DbContext;

public class DapperContext

{
    private readonly string connectionString = $"Server = localhost; port = 5432; database = rentacardb; User Id=postgres;password=12345";

    public Task<NpgsqlConnection> GetConnectionAsync()
    {
        return Task.FromResult(new NpgsqlConnection(connectionString));
    }
}