using firstAPI.Data;
using firstAPI.Models;

namespace firstAPI.Repository.IRepository
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetAll();
        Task<Country> GetById(int id);
        Task Create(Country entity);
        Task Update(Country entity);
        Task Delete(Country entity);
        Task Save();
        bool IsCountryExsits(string name);
    }
}
