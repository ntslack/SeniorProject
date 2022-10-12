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
    }
}