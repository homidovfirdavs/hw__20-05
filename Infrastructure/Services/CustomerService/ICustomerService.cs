using Domain.ApiResponse;
using Domain.Entities;

namespace Infrastructure.Services.CustomerService;

public interface ICustomerService
{
    Task<Response<List<Customer>>> GetCustomersAsync();
    Task<Response<Customer>> GetCustomerByIdAsync(int id);
    Task<Response<string>> AddCustomerAsync(Customer customer);
    Task<Response<string>> UpdateCustomerAsync(Customer customer);
    Task<Response<string>> DeleteCustomerAsync(int id);
}