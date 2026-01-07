using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using core8_angular_mssql.Services;
using core8_angular_mssql.Models.dto;
using QuestPDF.Infrastructure;
using core8_angular_mssql.Helpers;
using core8_angular_mssql.Entities;
using QuestPDF.Fluent; 

namespace core8_angular_mssql.Controllers.Products
{
    [ApiController]
    [Route("[controller]")]
    public class PdfReportController : ControllerBase {

        private readonly IWebHostEnvironment _env;
        private IProductService _productService;
        private IMapper _mapper;

        public PdfReportController(
            IWebHostEnvironment env,
            IMapper mapper,
            IProductService productService)
        {
            _env = env;
            _productService = productService;
            _mapper = mapper;
        }  

        [HttpGet("/products/report")]
        public async Task<IActionResult> ProductsReport() 
        {
            var products = await _productService.PdfDataAsync();
            var model = _mapper.Map<IEnumerable<Product>>(products); 
            var document = new ProductReportDocument(_env, model);
    
            byte[] pdfBytes = document.GeneratePdf();
            
            var reportId = DateTime.Now.Ticks; 
            return File(pdfBytes, "application/pdf", $"ProductReport_{reportId}.pdf");
        }
    }
}