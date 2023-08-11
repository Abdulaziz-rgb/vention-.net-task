using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_task.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }

    public string Username { get; set; }
    
    public int Age { get; set; }
    
    public string City { get; set; }
    
    //[Phone(ErrorMessage = "The phone number is not valid")]
    public int Phonenumber { get; set; }
    
    [EmailAddress(ErrorMessage = "The email address is not valid")]
    public string Email { get; set; }
}