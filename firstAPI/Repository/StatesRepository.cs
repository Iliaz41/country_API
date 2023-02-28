using firstAPI.Data;
using firstAPI.Models;
using firstAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace firstAPI.Repository
{
    public class StatesRepository : IStatesRepository
    {
        private readonly ApplicationDbContext _dbcontext;
        public StatesRepository(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }
        public async Task Create(States entity)
        {
            await _dbcontext.States.AddAsync(entity);
            await Save();
        }
        public async Task Delete(States entity)
        {
            _dbcontext.States.Remove(entity);
            await Save();
        }
        public async Task<List<States>> GetAll()
        {
            List<States> states = await _dbcontext.States.ToListAsync();
            return states;
        }
        public async Task<States> GetById(int id)
        {
            States state = await _dbcontext.States.FindAsync(id);
            return state;
        }
        public bool IsStateExsits(string name)
        {
            var result = _dbcontext.States.AsQueryable().Where(x => x.Name.ToLower().Trim() == name.ToLower().Trim()).Any();
            return result;
        }
        public async Task Save()
        {
            await _dbcontext.SaveChangesAsync();
        }
        public async Task Update(States entity)
        {
            _dbcontext.States.Update(entity);
            await Save();
        }
    }
}
