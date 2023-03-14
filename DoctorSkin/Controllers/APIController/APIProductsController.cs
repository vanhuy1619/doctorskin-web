
using DoctorSkin.Models;
using DoctorSkin.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DoctorSkin.Controllers.APIController
{
    [ApiController]
    [Route("api/")]
    public class APIProductsController : ControllerBase
    {
        private readonly ProductRepository productRepository;

        public APIProductsController(ProductRepository re)
        {
            productRepository = re;
        }

        [HttpGet("2")]
        public async Task<IActionResult> GetProducts()
        {
            var navbar = await productRepository.GetAllProductAsync();
            return Ok(navbar);
        }
    }
}