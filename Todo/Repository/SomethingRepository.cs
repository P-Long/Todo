using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Dtos;
using ToDo.Interfaces;
using ToDo.Migrations;
using ToDo.Models;


namespace ToDo.Repository
{
    public class SomethingRepository:ISomethingRepository
    {
        private readonly ApplicationDBContext _context;
        public SomethingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Models.Something> CreateAsync(Models.Something SomethingModel)
        {
            await _context.Somethings.AddAsync(SomethingModel);
            await _context.SaveChangesAsync();
            return SomethingModel;
        }

        public async Task<Models.Something?> DeleteAsync(int id)
        {
            var somethingModel = await _context.Somethings.FirstOrDefaultAsync(x => x.Id == id);
            if (somethingModel == null)
            {
                return null;
            }
            _context.Somethings.Remove(somethingModel);
            await _context.SaveChangesAsync();
            return somethingModel;
        }

        public async Task<List<Models.Something>> GetAllAsync()
        {
            var somethings = _context.Somethings.AsQueryable();
            return await somethings.ToListAsync();
        }

        public async Task<Models.Something?> GetByIdAsync(int id)
        {
            return await _context.Somethings.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Models.Something?> UpdateAsync(UpdateSomethingRequestDto somethingRequestDto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
