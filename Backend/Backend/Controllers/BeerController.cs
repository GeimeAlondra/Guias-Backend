using Backend.DTOs;
using Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private StoreContext _storeContext;

        public BeerController(StoreContext storeContext)
        {
            _storeContext = storeContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id) {
    
            var beer = await _storeContext.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            var beerDto = new BeerDto
            {
                Id = beer.BrandId,
                Al = beer.Al,
                BrandID = beer.BrandId,
                Name = beer.BeerName
            };
            return Ok(beerDto);
        }
    }
}
