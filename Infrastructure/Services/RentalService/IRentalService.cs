using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Services.RentalService;

public interface IRentalService
{
    Task<Response<List<Rental>>> GetCustomersAsync();
    Task<Response<Rental>> GetRentalByIdAsync(int id);
    Task<Response<string>> AddRentalAsync(Rental rental);
    Task<Response<string>> UpdateRentalAsync(Rental rental);
    Task<Response<string>> DeleteRentalAsync(int id);
}