using System.Collections.Generic;

namespace CarMeetApp.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
        public List<Car> Cars { get; set; }
    }
}