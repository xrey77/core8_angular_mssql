using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace core8_angular_mssql.Entities
{   
 [Table("sales")]
 public class Sale 
 {
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column(TypeName = "decimal(18,2)")] 
    public decimal Amount { get; set; }

    [Column(TypeName = "datetime2")]
    public DateTime Date { get; set; }
 }
}