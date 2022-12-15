using System.Collections.Generic;
using System.Threading.Tasks;
using CarMeetApp.Domain.Models;

namespace CarMeetApp.Domain.Abstractions.Repositories
{
    public interface IUsersRepositories
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        Task<User> CreateUserAsync(User newUser);
        Task<User> UpdateUserAsync(User updatedUser, int userId);
        Task<User> DeleteUserByIdAsync(int userId);

        Task<List<Car>> GetAllUserCarsAsync(int userId);
        Task<Car> GetUserCarByIdAsync(int userId, int carId);
        Task<Car> CreateUserCar(int userId, Car newCar);
        Task<Car> UpdateUserCar(int userId, Car updatedCar);
    }
}