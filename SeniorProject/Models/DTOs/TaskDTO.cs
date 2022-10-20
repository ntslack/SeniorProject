using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class TaskDTO
    {
        [Key]
        public int taskID { get; set; }

        public string? taskValue { get; set; }

        public DateTime? taskCreationDate { get; set; }

        public bool taskIsFavorited { get; set; }

        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set }
    }
}
