using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Services.RentalService;
namespace WebApplication1.Controllers;

[ApiController]
[Route("[controller]")]
public class RentalController(IRentalService rentalService) : Controller
{
   
}


