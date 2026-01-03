using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core8_angular_mssql.Entities
{
    [Table("roles")]
    public class Role {

        [Key]
        public int Id {get; set;}

        [Column("name",TypeName="varchar(20)")]
        [Required]
        public string Name {get; set;}

        public ICollection<User> Users { get; set; } = new List<User>();
    }
    

}