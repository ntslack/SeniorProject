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

        [Required(ErrorMessage = "Please Enter Username")]
        public string? username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [DataType(DataType.Password)]
        public string? password { get; set; }

        [Required(ErrorMessage = "Please Enter First Name")]
        public string? firstName { get; set; }

        [Required(ErrorMessage = "Please Enter Last Name")]
        public string? lastName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        //[RegularExpression(@"[a-zA-Z0-9_]+@[A-Za-z0-9]+.[A-Za-z]{2,4}")]
        [DataType(DataType.EmailAddress)]
        public string? email { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
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
