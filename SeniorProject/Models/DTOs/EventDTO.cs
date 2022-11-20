using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class EventDTO
    {
        [Key]
        public int eventID { get; set; }

        public string? eventTitle { get; set; }

        public string? eventDescription { get; set; }

        public DateTime? eventCreationDate { get; set; } = DateTime.Now;

        public DateTime? eventStartTime { get; set; } = DateTime.Now;

        public DateTime? eventEndTime { get; set; } = DateTime.Now;

        public bool eventIsFavorited { get; set; }
        
        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set; }
    }
}
