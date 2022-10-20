using Microsoft.EntityFrameworkCore;
using Castle.Components.DictionaryAdapter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using KeyAttribute = System.ComponentModel.DataAnnotations.KeyAttribute;

namespace SeniorProject.Models.DTOs
{
    public class ExpenseDTO
    {
        [Key]
        public int expenseID { get; set; }

        public string? expenseTitle { get; set; }

        public string? expenseDescription { get; set; }

        public int expenseValue { get; set; }

        public DateTime? expenseCreationDate { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public int userID { get; set; }

        public UserAccount? User { get; set; }
    }
}
