using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using RecruitmentToDoApp.Models;

namespace RecruitmentToDoApp.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoAppContext toDoAppContext;
        private readonly ILogger<ToDoService> logger;
        private const int DefaultPageSize = 10;

        public ToDoService(IToDoAppContext toDoAppContext, ILogger<ToDoService> logger)
        {
            this.toDoAppContext = toDoAppContext;
            this.logger = logger;
        }

        public async Task<bool> CreateToDoAsync(ToDo toDo)
        {
            try
            {
                await toDoAppContext.ToDos.AddAsync(toDo);
                await toDoAppContext.SaveChangesAsync();
                return true;
            }
            catch (MySqlException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<ToDo> GetToDoByIdAsync(int id)
        {
            try
            {
                var a = toDoAppContext.ToDos.ToList();
                var result = await toDoAppContext.ToDos.SingleAsync(t => t.Id == id);

                return result;
            }
            catch (MySqlException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return null;
            }
        }

        public async Task<IList<ToDo>> GetToDosAsync(ToDoParams filter)
        {
            try
            {
                var query = toDoAppContext.ToDos.AsQueryable();

                if (filter.MaxExpiryDate.HasValue)
                    query = query.Where(t => t.ExpiryDate <= filter.MaxExpiryDate.Value);
                if (filter.MinExpiryDate.HasValue)
                    query = query.Where(t => t.ExpiryDate >= filter.MinExpiryDate.Value);
                if (filter.PageNumber.HasValue)
                {
                    if (!filter.PageSize.HasValue)
                        filter.PageSize = DefaultPageSize;

                    query = query.Skip((filter.PageNumber.Value - 1) * filter.PageSize.Value)
                        .Take(filter.PageSize.Value);
                }

                return await query.ToListAsync();
            }
            catch (MySqlException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return new List<ToDo>();
            }
        }

        public async Task<bool> UpdateToDoAsync(int id, ToDo toDo)
        {
            try
            {
                var oldToDo = await toDoAppContext.ToDos.SingleAsync(t => t.Id == id);

                oldToDo.ExpiryDate = toDo.ExpiryDate;
                oldToDo.Description = toDo.Description;
                oldToDo.CompletePercentage = toDo.CompletePercentage;
                oldToDo.Title = toDo.Title;

                await toDoAppContext.SaveChangesAsync();
                return true;
            }
            catch (MySqlException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> DeleteToDoAsync(int id)
        {
            try
            {
                var toRemove = await toDoAppContext.ToDos.SingleAsync(t => t.Id == id);

                toDoAppContext.ToDos.Remove(toRemove);

                await toDoAppContext.SaveChangesAsync();
                return true;
            }
            catch (MySqlException ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
