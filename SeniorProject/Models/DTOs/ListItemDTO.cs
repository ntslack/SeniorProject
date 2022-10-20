using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class ListItemDTO
    {
        [Key]
        public int listItemID { get; set; }

        public string? listItemValue { get; set; }

        public DateTime? listItemCreationDate { get; set; } = DateTime.Now;

        [ForeignKey("List")]
        public int listID { get; set; }

        public ListDTO? List { get; set; }
    }
}
