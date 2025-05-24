using Domain.ApiResponse;
using Domain.Entities;
using Infrastructure.Services.CustomerService;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;
[ApiController]
[Route("[controller]")]
public class CustomerController(ICustomerService _customerService) : Controller
{
    // GET
    [HttpGet("getall")]
    public async Task<Response<List<Customer>>> GetCustomersAsync()
    {
        var result = await _customerService.GetCustomersAsync();
        return result;
    }
    
    [HttpGet("getbyid/{id}")]
    public async Task<Response<Customer>> GetCustomerByIdAsync(int id)
    {
        var result = await _customerService.GetCustomerByIdAsync(id);
        return result;
    }
    
    [HttpPost("addcustomer")]
    public async Task<Response<string>> AddCustomerAsync(Customer customer)
    {
        var result = await _customerService.AddCustomerAsync(customer);
        return result;
    }
    
    [HttpPut("updatecustomer")]
    public async Task<Response<string>> UpdateCustomerAsync(Customer customer)
    {
        var result = await _customerService.UpdateCustomerAsync(customer);
        return result;
    }
    
    [HttpDelete("deletecustomer/{id}")]
    public async Task<Response<string>> DeleteCustomerAsync(int id)
    {
        var result = await _customerService.DeleteCustomerAsync(id);
        return result;
    }
}