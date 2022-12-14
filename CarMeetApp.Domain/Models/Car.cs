namespace CarMeetApp.Domain.Models
{
    public class Car
    {
        public int CarId { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int Hp { get; set; }
        
        // Navigation props
        public int UserId { get; set; }
        public User User { get; set; }
    }
}