using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace core8_angular_mssql.Models.dto
{
  public class UserPasswordUpdate
    {        
        public string Password { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    
}