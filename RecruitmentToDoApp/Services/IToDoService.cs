using RecruitmentToDoApp.Models;

namespace RecruitmentToDoApp.Services
{
    public interface IToDoService
    {
        Task<bool> CreateToDoAsync(ToDo toDo);
        Task<ToDo> GetToDoByIdAsync(int id);
        Task<IList<ToDo>> GetToDosAsync(ToDoParams filter);
        Task<bool> UpdateToDoAsync(int id, ToDo toDo);
        Task<bool> DeleteToDoAsync(int id);
    }
}