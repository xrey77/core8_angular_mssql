using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using core8_angular_mssql.Services;
using core8_angular_mssql.Models.dto;
using core8_angular_mssql.Helpers;

namespace core8_angular_mssql.Controllers.Products
{
    [ApiExplorerSettings(GroupName = "List All Products")]
    [ApiController]
    [Route("[controller]")]
    public class ListController : ControllerBase {

        private IProductService _productService;

        private IMapper _mapper;
        private readonly IConfiguration _configuration;  

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<ListController> _logger;

        public ListController(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<ListController> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpGet("/api/listproducts/{page}")]
        public IActionResult ListProducts(int page) {
            try {                
                int totalpage = _productService.TotPage();
                var products = _productService.ListAll(page);
                if (products != null) {
                    var model = _mapper.Map<IList<ProductModel>>(products);
                    return Ok(new {totpage = totalpage, page = page, products=model});
                } else {
                    return BadRequest(new {statuscode=404, message="No Data found."});
                }
            } catch(AppException ex) {
               return BadRequest(new {statuscode = 404, Message = ex.Message});
            }
        }
    }    
}