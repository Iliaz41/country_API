using firstAPI.Models;

namespace firstAPI.Repository.IRepository
{
    public interface IStatesRepository
    {
        Task<List<States>> GetAll();
        Task<States> GetById(int id);
        Task Create(States entity);
        Task Update(States entity);
        Task Delete(States entity);
        Task Save();
        bool IsStateExsits(string name);
    }
}
