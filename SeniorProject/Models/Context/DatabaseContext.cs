using Microsoft.EntityFrameworkCore;
using SeniorProject.Models;
using SeniorProject.Models.DTOs;

namespace SeniorProject.Models.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<UserAccount>? User { get; set; }
        public DbSet<NotesDTO>? Note { get; set; }
        public DbSet<ReminderDTO>? Reminder { get; set; }
        public DbSet<TaskDTO>? Task { get; set; }
        public DbSet<ListDTO>? List { get; set; }
        public DbSet<ListItemDTO>? ListItem { get; set; }
        public DbSet<ExpenseDTO>? Expense { get; set; }
        public DbSet<EventDTO>? Event { get; set; }
    }
}