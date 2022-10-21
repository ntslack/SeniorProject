using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class ReminderDTO
    {
        [Key]
        public int reminderID { get; set; }

        public string? reminderTitle { get; set; }

        public string? reminderDescription { get; set; }

        public DateTime? reminderCreationDate { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set; }
    }
}
