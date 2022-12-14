using System.Collections.Generic;
using CarMeetApp.Domain.Models;

namespace CarMeetApp.Dal
{
    public class DataSource
    {
        public List<User> Users { get; set; }
        

        public DataSource()
        {
            var cars = new List<Car>()
            {
                new Car() {Make = "Ford", Model = "Focus RS", Year = 2016, Hp = 410},
                new Car() {Make = "Ford", Model = "Focus ST250", Year = 2018, Hp = 250},
            };
            Users = new List<User>()
            {
                new User() {Firstname = "Marius", Lastname = "Gravningsmyhr", Username = "mgo", Cars = cars}
            };
        }
    }
}