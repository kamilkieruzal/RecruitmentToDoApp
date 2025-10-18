using Microsoft.EntityFrameworkCore;
using RecruitmentToDoApp.Models;

namespace RecruitmentToDoApp.Services
{
    public interface IToDoAppContext
    {
        DbSet<ToDo> ToDos { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}