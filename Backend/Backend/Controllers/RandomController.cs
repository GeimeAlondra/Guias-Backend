using Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class RandomController : ControllerBase
    {
        private IRandomServices _rSingleton;
        private IRandomServices _rScope;
        private IRandomServices _rTransient;

        private IRandomServices _rSingleton2;
        private IRandomServices _rScope2;
        private IRandomServices _rTransient2;

        public RandomController(
            [FromKeyedServices("randomSingleton")] IRandomServices randomSingleton,
             [FromKeyedServices("randomScope")] IRandomServices randomScope,
              [FromKeyedServices("randomTransient")] IRandomServices randomTransient,
               [FromKeyedServices("randomSingleton")] IRandomServices randomSingleton2,
                [FromKeyedServices("randomScope")] IRandomServices randomScope2,
                 [FromKeyedServices("randomTransient")] IRandomServices randomTransient2
            )
        {
            _rSingleton = randomSingleton;
            _rTransient = randomTransient;
            _rScope = randomScope;
            _rSingleton2 = randomSingleton2;
            _rTransient2 = randomTransient2;
            _rScope2 = randomScope2;
        }

        [HttpGet]
        public ActionResult<Dictionary<string, int>> Get()
        {
            var result = new Dictionary<string, int>();
            result.Add("Singleton", _rSingleton.Value);
            result.Add("Scope", _rScope.Value);
            result.Add("Transient", _rTransient.Value);
            result.Add("Singleton2", _rSingleton2.Value);
            result.Add("Scope2", _rScope2.Value);
            result.Add("Transient2", _rTransient2.Value);
            return result;
        }
    }
}
