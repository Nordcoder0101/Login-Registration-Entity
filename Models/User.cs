using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace LogReg.Models
{
  public class User
  {
    [Key]
    public int Id{get;set;}
    [Required]
    [MinLength(3)]
    public string FirstName{get;set;}
    [Required]
    [MinLength(3)]
    public string LastName{get;set;}
    [EmailAddress]
    [Required]
    public string Email{get;set;}
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password{get;set;}
    
    [Compare("Password")]
    [DataType(DataType.Password)]
    [NotMapped]
    public string Confirm{get;set;}
    public DateTime CreatedAt {get;set;} = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User() {}
  }
  
}