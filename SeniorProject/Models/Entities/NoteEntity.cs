using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.Entities
{
    public class NoteEntity
    {
        [Key]
        public int noteID { get; set; }

        public string? noteTitle { get; set; }

        public string? noteValue { get; set; }

        public DateTime noteCreationDate { get; set; } = DateTime.Now;

        public bool noteIsFavorited { get; set; }

        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set; }
    }
}
