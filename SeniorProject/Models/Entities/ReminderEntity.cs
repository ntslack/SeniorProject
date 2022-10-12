namespace SeniorProject.Models.Entities
{
    public class ReminderEntity
    {
        public int reminderID { get; set; }
        public int userID { get; set; }
        public string? reminderTitle { get; set; }
        public string? reminderDescription { get; set; }
        public DateTime? reminderDate { get; set; }
    }
}
