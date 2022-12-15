using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarMeetApp.Api.Dtos;
using CarMeetApp.Dal;
using CarMeetApp.Domain.Abstractions.Repositories;
using CarMeetApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarMeetApp.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly IUsersRepositories _userRepo;


        public UsersController(DataContext ctx, IMapper mapper, IUsersRepositories userRepo)
        {
            _mapper = mapper;
            _userRepo = userRepo;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepo.GetAllUsersAsync();

            var mappedUsers = _mapper.Map<List<UserReadDto>>(users);
            return Ok(mappedUsers);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _userRepo.GetUserByIdAsync(userId);

            if (user == null) return NotFound();

            var mappedUser = _mapper.Map<UserReadDto>(user);

            return Ok(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto newUser)
        {
            var domainUser = _mapper.Map<User>(newUser);

            await _userRepo.CreateUserAsync(domainUser);

            var mappedUser = _mapper.Map<UserReadDto>(domainUser);

            return CreatedAtAction(nameof(GetUserById), new {UserId = domainUser.UserId}, mappedUser);
        }
        
        // Update
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updateUser, int userId)
        {
       

            var mappedUser = _mapper.Map<User>(updateUser);
            mappedUser.UserId = userId;

            await _userRepo.UpdateUserAsync(mappedUser,userId);

            return NoContent();
        }


        // Delete
        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var userToDelete = _userRepo.DeleteUserByIdAsync(userId);
            if (userToDelete == null) return NotFound();

            return NoContent();
        }

        [HttpGet("{userId}/cars")]
        public async Task<IActionResult> GetAllUserCars(int userId)
        {
            var cars = await _userRepo.GetAllUserCarsAsync(userId);

            var mappedCars = _mapper.Map<List<CarReadDto>>(cars);

            return Ok(mappedCars);
        }

        [HttpGet("{userId}/cars/{carId}")]
        public async Task<IActionResult> GetUserCarById(int userId, int carId)
        {
            var car = await _userRepo.GetUserCarByIdAsync(userId, carId);

            if (car == null) return NotFound();

            var mappedCar = _mapper.Map<CarReadDto>(car);

            return Ok(mappedCar);


        }

        [HttpPost("{userId}/cars")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto newCar, int userId)
        {
            var mappedCar = _mapper.Map<Car>(newCar);

            var car = await _userRepo.CreateUserCar(userId, mappedCar);
            if (car == null) return NotFound();

            var carReadDto = _mapper.Map<CarReadDto>(mappedCar);
            
            return CreatedAtAction(nameof(GetUserCarById), new {UserId = userId, CarId = carReadDto.CarId}, carReadDto);
        }

        [HttpPut("{userId}/cars/{carId}")]
        public async Task<IActionResult> UpdateUserCar([FromBody] CarUpdateDto updatedCar, int userId, int carId)
        {
            var mappedCar = _mapper.Map<Car>(updatedCar);
            mappedCar.CarId = carId;
            mappedCar.UserId = userId;

            var car = await _userRepo.UpdateUserCar(userId, mappedCar);
            if (car == null) return NotFound();

            return NoContent();
        }
        
    }
}