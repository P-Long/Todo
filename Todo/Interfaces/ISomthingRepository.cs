using ToDo.Dtos;
using ToDo.Migrations;

namespace ToDo.Interfaces
{
    public interface ISomethingRepository
    {
        Task<List<Models.Something>> GetAllAsync();
        Task<Models.Something?> GetByIdAsync(int id);
        Task<Models.Something> CreateAsync(Models.Something SomethingModel);
        Task<Models.Something?> UpdateAsync(UpdateSomethingRequestDto somethingRequestDto, int id);
        Task<Models.Something?> DeleteAsync(int id);
    }   
}
