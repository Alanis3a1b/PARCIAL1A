using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PARCIAL1A.Models;

namespace PARCIAL1A.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class librosController : ControllerBase
    {
        private readonly librosContext _librosContext;

        public librosController(librosContext librosContext)
        {
            _librosContext = librosContext;
        }
    }
}
