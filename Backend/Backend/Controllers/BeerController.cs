﻿using Backend.DTOs;
using Backend.Models;
using Backend.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private IValidator<BeerUpdateDto> _beerUpdateValidator;

        private ICommonBeerServices<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public BeerController(
             StoreContext storeContext,
             IValidator<BeerInsertDto> beerInsertValidators,
             IValidator<BeerUpdateDto> beerUpdateValidators,
             [FromKeyedServices("beerService")]ICommonBeerServices<BeerDto, BeerInsertDto, BeerUpdateDto> beerServices)
        {
            _beerInsertValidator = beerInsertValidators;
            _beerUpdateValidator = beerUpdateValidators;
            _beerService = beerServices;
        }

        [HttpGet]
        public async Task<IEnumerable<BeerDto>> Get() => await _beerService.Get();

        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpPost]
        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validationREsult = await _beerInsertValidator.ValidateAsync(beerInsertDto);

            if (!validationREsult.IsValid)
            {
                return BadRequest(validationREsult.Errors);
            }

            var beerDto = await _beerService.Add(beerInsertDto);

            return CreatedAtAction(nameof(GetById), new { id = beerDto.Id }, beerDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var validationResult = await _beerUpdateValidator.ValidateAsync(beerUpdateDto);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var beerDto = await _beerService.Update(id, beerUpdateDto);

            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beerDto = await _beerService.Delete(id);
            
            return beerDto == null ? NotFound() : Ok(beerDto);
        }
    }
}
