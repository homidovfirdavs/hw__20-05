using Dapper;
using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.DbContext;

namespace Infrastructure.Services.RentalService;

public class RentalService : IRentalService
{
    public Task<Response<List<Rental>>> GetCustomersAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Response<Rental>> GetRentalByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> AddRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> UpdateRentalAsync(Rental rental)
    {
        throw new NotImplementedException();
    }

    public Task<Response<string>> DeleteRentalAsync(int id)
    {
        throw new NotImplementedException();
    }
}
    
 