namespace SeniorProject.Models.DTOs
{
    public class ReminderDTO
    {
        public int Id { get; set; }
        public int userID { get; set; }
        public string? reminderTitle { get; set; }
        public string? reminderDescription { get; set; }
        public DateTime? reminderCreationDate { get; set; }
    }
}
