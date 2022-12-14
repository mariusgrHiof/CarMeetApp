using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarMeetApp.Api.Dtos;
using CarMeetApp.Dal;
using CarMeetApp.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarMeetApp.Api.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly DataContext _ctx;
        private readonly IMapper _mapper;


        public UsersController(DataContext ctx, IMapper mapper)
        {
            _ctx = ctx;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _ctx.Users.Include(u => u.Cars).ToListAsync();

            var mappedUsers = _mapper.Map<List<UserReadDto>>(users);
            return Ok(mappedUsers);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return NotFound();

            var mappedUser = _mapper.Map<UserReadDto>(user);

            return Ok(mappedUser);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto newUser)
        {
            var domainUser = _mapper.Map<User>(newUser);
            _ctx.Users.Add(domainUser);
            await _ctx.SaveChangesAsync();

            var mappedUser = _mapper.Map<UserReadDto>(domainUser);

            return CreatedAtAction(nameof(GetUserById), new {UserId = domainUser.UserId}, mappedUser);
        }
        
        // Update
        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateDto updateUser, int userId)
        {
       

            var mappedUser = _mapper.Map<User>(updateUser);
            mappedUser.UserId = userId; 
            
            _ctx.Users.Update(mappedUser);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }


        // Delete
        [HttpDelete("userId")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _ctx.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user == null) return NotFound();

            _ctx.Users.Remove(user);
            await _ctx.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("{userId}/cars")]
        public async Task<IActionResult> GetAllUserCars(int userId)
        {
            var cars = await _ctx.Cars.Where(c => c.UserId == userId).ToListAsync();

            var mappedCars = _mapper.Map<List<CarReadDto>>(cars);

            return Ok(mappedCars);
        }

        [HttpGet("{userId}/cars/{carId}")]
        public async Task<IActionResult> GetUserCarById(int userId, int carId)
        {
            var user = await _ctx.Users.Include(u => u.Cars).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound();

            var car = user.Cars.FirstOrDefault(c => c.CarId == carId);
            if (car == null) return NotFound();

            var mappedCar = _mapper.Map<CarReadDto>(car);

            return Ok(mappedCar);


        }

        [HttpPost("{userId}/cars")]
        public async Task<IActionResult> CreateCar([FromBody] CarCreateDto newCar, int userId)
        {
            var mappedCar = _mapper.Map<Car>(newCar);
            
            var user = await _ctx.Users.Include(u => u.Cars).FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) return NotFound();
            
            user.Cars.Add(mappedCar);
            await _ctx.SaveChangesAsync();

            var carReadDto = _mapper.Map<CarReadDto>(mappedCar);
            
            return CreatedAtAction(nameof(GetUserCarById), new {UserId = userId, CarId = carReadDto.CarId}, carReadDto);
        }
    }
}