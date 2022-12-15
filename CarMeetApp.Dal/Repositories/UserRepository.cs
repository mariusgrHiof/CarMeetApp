using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarMeetApp.Domain.Abstractions.Repositories;
using CarMeetApp.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace CarMeetApp.Dal.Repositories
{
    public class UserRepository : IUsersRepositories
    {
        private readonly DataContext _ctx;

        public UserRepository(DataContext ctx)
        {
            _ctx = ctx;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _ctx.Users.Include(u => u.Cars).ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return null;

            return user;
        }

        public async Task<User> CreateUserAsync(User user)
        {
            _ctx.Users.Add(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task<User> UpdateUserAsync(User updatedUser, int userId)
        {
            
            _ctx.Users.Update(updatedUser);
            await _ctx.SaveChangesAsync();

            return updatedUser;
        }

        public async Task<User> DeleteUserByIdAsync(int userId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return null;

            _ctx.Users.Remove(user);
            await _ctx.SaveChangesAsync();

            return user;
        }

        public async Task<List<Car>> GetAllUserCarsAsync(int userId)
        {
            var cars = await _ctx.Cars.Where(c => c.UserId == userId).ToListAsync();

            return cars;


        }

        public async Task<Car> GetUserCarByIdAsync(int userId, int carId)
        {
            var user = await _ctx.Users
                .Include(u => u.Cars)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return null;

            var car = user.Cars.FirstOrDefault(c => c.CarId == carId);
            if (car == null) return null;

            return car;
        }

        public async Task<Car> CreateUserCar(int userId, Car newCar)
        {
            var user = await _ctx.Users
                .Include(u => u.Cars)
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return null;
            
            user.Cars.Add(newCar);
            await _ctx.SaveChangesAsync();

            return newCar;
        }

        public async Task<Car> UpdateUserCar(int userId, Car updatedCar)
        {
            _ctx.Cars.Update(updatedCar);
            await _ctx.SaveChangesAsync();

            return updatedCar;
        }
    }
}