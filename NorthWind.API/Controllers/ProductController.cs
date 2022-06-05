using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthWind.Core.Models;
using NorthWind.Core.Services;

namespace NorthWind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IService<Products> _service;

        public ProductController(IService<Products> service)
        {
            _service = service;
        }
    }
}
