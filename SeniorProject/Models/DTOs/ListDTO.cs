using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class ListDTO
    {
        [Key]
        public int listID { get; set; }

        public string? listName { get; set; }

        public string? listDescription { get; set; }

        public bool listIsFavorited { get; set; }

        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set; }

        public ICollection<ListItemDTO>? ListItems { get; set; }
    }
}
