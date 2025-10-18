using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecruitmentToDoApp.Models
{
    /// <summary>
    /// Minimal ToDo structure
    /// </summary>
    [Table("ToDo")]
    public class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public DateTime ExpiryDate { get; set; }
        [Required]
        public string? Description { get; set; }
        [Range(0, 100)]
        public int CompletePercentage { get; set; } = 0;
    }
}
