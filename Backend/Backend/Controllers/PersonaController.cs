using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonaController : Controller
    {
        [HttpGet("all")]
        public List<PersonaDatos> GetPersonaDatos() => Repository.persona;
    }
}
