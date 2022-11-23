using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SeniorProject.Models.DTOs;

namespace SeniorProject.Models
{
    public class UserAccount
    {
        [Key]
        public int userID { get; set; }

        public string? username { get; set; }

        [DataType(DataType.Password)]
        public string? password { get; set; }

        public string? firstName { get; set; }

        public string? lastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? mobile { get; set; }

        public bool isAdmin { get; set; }

        public ICollection<NotesDTO>? Notes;

        public ICollection<ReminderDTO>? Reminders;

        public ICollection<TaskDTO>? Tasks;

        public ICollection<ListDTO>? Lists;

        public ICollection<ExpenseDTO>? Expenses;

        public ICollection<EventDTO>? Events;
    }
}
