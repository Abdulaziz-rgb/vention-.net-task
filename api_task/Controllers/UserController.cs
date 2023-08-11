using api_task.Models;
using CsvHelper;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using api_task.Interface;
using api_task.Mapper;

namespace api_task.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private static IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost]
        [Route("upload")]
        public IActionResult UploadUsers(IFormFile file)
        {    
            var fileExtension = Path.GetExtension(file.FileName);

            if (fileExtension != ".csv")
            {
                return BadRequest("Choose only csv files!");
            }
 
            if (file == null || file.Length <= 0)
                return BadRequest("Invalid file!");
    
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Context.RegisterClassMap<CsvDataMapper>();
                    var newUsers = csv.GetRecords<User>().ToList();
                    foreach (var newUser in newUsers)
                    {
                        var existingUser = _userRepository.FindUser(newUser);
                        if (existingUser != null)
                        {
                            // Update existing user record
                            UpdateUser(existingUser, newUser);
                        }
                        else
                        {
                            // Add new user record
                            _userRepository.AddUser(newUser);
                        }
                    }

                    return Ok("Users uploaded successfully.");
                }
        }

        [HttpGet]
        [Route("users")]
        public IActionResult Users(string sortDirection = "asc", int limit = 100) 
        {
            var sortedUsers = _userRepository.GetUserList();
            if (sortDirection.ToLower() == "desc")
                sortedUsers = sortedUsers.Reverse();
            var limitedUsers = sortedUsers.Take(limit).ToList();

            return Ok(limitedUsers);

        }

        [HttpDelete]
        [Route("deleteAllUsers")]
        public IActionResult DeleteAll()
        {
            _userRepository.CleanTable();

            return Ok("User list deleted successfully");
        }
        
        private void UpdateUser(User existingUser, User newUser)
        {
            existingUser.Age = newUser.Age;
            existingUser.City = newUser.City;
            existingUser.Phonenumber = newUser.Phonenumber;
            existingUser.Email = newUser.Email;
            existingUser.Username = newUser.Username;
            
            _userRepository.UpdateUser(existingUser);
        }
    }
}
