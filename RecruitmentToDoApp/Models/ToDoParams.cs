using System.ComponentModel.DataAnnotations;

namespace RecruitmentToDoApp.Models
{
    public class ToDoParams
    {
        public DateTime? MaxExpiryDate { get; set; }
        public DateTime? MinExpiryDate { get; set; }
        [Range(1, 100)]
        public int? PageSize { get; set; } = 10;
        [Range(1, int.MaxValue)]
        public int? PageNumber { get; set; }
    }
}