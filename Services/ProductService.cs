using core8_angular_mssql.Entities;
using core8_angular_mssql.Helpers;
using core8_angular_mssql.Models;
using core8_angular_mssql.Models.dto;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace core8_angular_mssql.Services
{
    public interface IProductService {
        IEnumerable<Product> ListAll(int page);
        Task<IEnumerable<Product>> PdfDataAsync();       
        Task<IEnumerable<SaleSummaryDto>> SalesDataAsync();       
        IEnumerable<Product> SearchAll(string key);
        IEnumerable<Product> Dataset();

        int TotPage();
    }

    public class ProductService : IProductService
    {

        private DataDbContext _context;
        private readonly AppSettings _appSettings;


        public ProductService(DataDbContext context,IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }        
        public int TotPage() {
            var perpage = 5;
            var totrecs = _context.Products.Count();
            int totpage = (int)Math.Ceiling((float)(totrecs) / perpage);
            return totpage;
        }

        public async Task<IEnumerable<SaleSummaryDto>> SalesDataAsync()
        {
            var salesData = await _context.Sales
                .GroupBy(s => s.Date.Month)
                .Select(g => new SaleSummaryDto
                { 
                    Month = g.Key, 
                    Total = Convert.ToDouble(g.Sum(s => s.Amount)) 
                })
                .OrderBy(x => x.Month)
                .ToListAsync();

            return salesData;
        }

        // public async Task<IEnumerable<SaleSummaryDto>> SalesDataAsync()
        // {
        //     var salesData = await _context.Sales
        //         .GroupBy(s => s.Date.Month)
        //         .Select(g => new SaleSummaryDto
        //         { 
        //             Month = g.Key, 
        //             Total = (double)g.Sum(s => s.Amount) 
        //         })
        //         .OrderBy(x => x.Month)
        //         .ToListAsync();

        //     return salesData;
        // }


        // public async Task<IEnumerable<Sale>> SalesDataAsync()
        // {
        //     // var sales = await _context.Sales.ToListAsync();

        //     var salesData = await _context.Sales
        //         .GroupBy(s => s.Date.Month)
        //         .Select(g => new { Month = g.Key, Total = (double)g.Sum(s => s.Amount) })
        //         .OrderBy(x => x.Month)
        //         .ToListAsync();

        //     double[] values = salesData.Select(x => x.Total).ToArray();
        //     double[] positions = salesData.Select(x => (double)x.Month).ToArray();

        //     return salesData;
        // }

        public async Task<IEnumerable<Product>> PdfDataAsync()
        {
            var products = await _context.Products.ToListAsync();
            return products;
        }
    
        public IEnumerable<Product> ListAll(int page)
        {
            var perpage = 5;
            var offset = (page -1) * perpage;

            var products = _context.Products                                
                .OrderBy(b => b.Id)
                .Skip(offset)
                .Take(perpage)
                .ToList();

            return products;
        }

        public IEnumerable<Product> SearchAll(string key)
        {
            var columnName = $"descriptions";
            var columnValue = new SqlParameter("columnValue", "%" + key + "%");
            var sql = $"SELECT * FROM [Products] WHERE {columnName} LIKE @columnValue";
            var products = _context.Products.FromSqlRaw(sql, columnValue).ToList();
            return products;
        }

        public IEnumerable<Product> Dataset()
        {
            var products = _context.Products.ToList();
            return products;
        }
    }
}