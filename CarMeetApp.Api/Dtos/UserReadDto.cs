using System.Collections.Generic;
using CarMeetApp.Domain.Models;

namespace CarMeetApp.Api.Dtos
{
    public class UserReadDto
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Username { get; set; }
    }
}