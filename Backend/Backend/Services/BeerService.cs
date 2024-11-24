using Backend.DTOs;

namespace Backend.Services
{
    public class BeerService : IBeerServices
    {
        Task<BeerDto> IBeerServices.Add(BeerInsertDto beerInsertDto)
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerServices.Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<BeerDto>> Get()
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerServices.GetById(int id)
        {
            throw new NotImplementedException();
        }

        Task<BeerDto> IBeerServices.Update(int id, BeerUpdateDto beerUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
