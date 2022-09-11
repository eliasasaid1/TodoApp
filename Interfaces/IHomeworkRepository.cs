
using TodoApp.Objects;

namespace TodoApp.Interfaces
{
    public interface IHomeworkRepository
    {
        Task<Homework> GetByIdAsync(int id);
        Task<IEnumerable<Homework>> GetAllAsync();
        Task<int> InsertAsync(Homework homework);
        Task<int> UpdateAsync(Homework homework);
        Task<int> DeleteAsync(Homework homework);
    }
}
