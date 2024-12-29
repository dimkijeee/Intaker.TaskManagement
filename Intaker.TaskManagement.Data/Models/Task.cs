using System.ComponentModel.DataAnnotations;

namespace Intaker.TaskManagement.Data.Models
{
    public class Task
    {
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public int Status { get; set; }
        public string? AssignedTo { get; set; }
    }
}
