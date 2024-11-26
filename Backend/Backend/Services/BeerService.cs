using AutoMapper;
using Backend.DTOs;
using Backend.Models;
using Backend.Repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Backend.Services
{
    public class BeerService : ICommonBeerServices<BeerDto, BeerInsertDto, BeerUpdateDto>
    {
        public List<string> Errors { get; }

        private IRepository<Beer> _beerRepository;
        private IMapper _mapper;

        List<string> ICommonBeerServices<BeerDto, BeerInsertDto, BeerUpdateDto>.Errors => throw new NotImplementedException();

        public BeerService(IRepository<Beer> beerRepository, IMapper mapper)
        {
            _beerRepository = beerRepository;
            _mapper = mapper;
            Errors = new List<string>();
        }

        public async Task<IEnumerable<BeerDto>> Get()
        {
            var beer = await _beerRepository.Get();
            return beer.Select(x => _mapper.Map<BeerDto>(x));
        }

        public async Task<BeerDto> GetById(int id)
        {
            var beer = await _beerRepository.GetById(id);

            if (beer != null)
            {
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Add(BeerInsertDto beerInsertDto)
        {
            var beer = _mapper.Map<Beer>(beerInsertDto);
            await _beerRepository.Add(beer);
            await _beerRepository.Save();

            var beerDto = _mapper.Map<BeerDto>(beer);
            return beerDto;
        }

        public async Task<BeerDto> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                beer = _mapper.Map<BeerUpdateDto, Beer>(beerUpdateDto, beer);
                //beer.BeerName = beerUpdateDto.Name;
                //beer.Al = beerUpdateDto.Al;
                //beer.BrandId = beerUpdateDto.BrandID;
             
                _beerRepository.Update(beer);
                await _beerRepository.Save();
                var beerDto = _mapper.Map<BeerDto>(beer);
                return beerDto;
            }
            return null;
        }

        public async Task<BeerDto> Delete(int id)
        {
            var beer = await _beerRepository.GetById(id);
            if (beer != null)
            {
                var beerDto = new BeerDto
                {
                    Id = beer.BeerId,
                    Name = beer.BeerName,
                    BrandID = beer.BrandId,
                    Al = beer.Al
                };
                _beerRepository.Delete(beer);
                await _beerRepository.Save();
                return beerDto;
            }
            return null;
        }

        public bool validate(BeerInsertDto beerInsertDto)
        {
            if (_beerRepository.Search(bdto => bdto.BeerName == beerInsertDto.Name).Count() > 0)
            {
                Errors.Add("Ya existe el nombre en la base de datos");
            }
            return true;
        }

        public bool validate(BeerUpdateDto beerUpdateDto)
        {
            if (_beerRepository.Search(bdto => bdto.BeerName == beerUpdateDto.Name
             && beerUpdateDto.Id != bdto.BeerId).Count() > 0)
            {
                Errors.Add("Ya existe el nombre en la base de datos");
                return false;
            }
            return true;
        }
    }
}
