using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace core8_angular_mssql.Models
{
    public class UploadfileModel {
        public int Id { get; set; }
        public IFormFile Profilepic { get; set; }

    }
    
}