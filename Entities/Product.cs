
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using core8_angular_mssql.Helpers;

namespace core8_angular_mssql.Entities
{
    
[Table("products")]
public class Product {

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("category")]
        public string Category { get; set; }

        [Column("descriptions")]
        public string Descriptions { get; set; }

        [Column("qty")]
        public int Qty { get; set; }

        [Column("unit")]
        public string Unit { get; set; }

        [Column("costprice",TypeName="decimal(10,2)")]
        public decimal CostPrice { get; set; }

        [Column("sellprice",TypeName="decimal(10,2)")]
        public decimal SellPrice { get; set; }

        [Column("saleprice",TypeName="decimal(10,2)")]
        public decimal SalePrice { get; set; }

        [Column("productpicture")]
        public string ProductPicture { get; set; }

        [Column("alertstocks")]
        public int AlertStocks { get; set; }

        [Column("criticalstocks")]
        public int CriticalStocks { get; set; }

        [Column("createdAt")]
        public DateTime CreatedAt { get; set; }

        [Column("updatedAt")]
        public DateTime UpdatedAt { get; set; }

        [NotMapped]
        public int TotalPage { get; set;}

        [NotMapped]
        public int Page { get; set; }
    }    
}