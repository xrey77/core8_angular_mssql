using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using core8_angular_mssql.Services;
using core8_angular_mssql.Models.dto;
using core8_angular_mssql.Helpers;
using core8_angular_mssql.Models;

namespace core8_angular_mssql.Controllers.Products
{
    [ApiExplorerSettings(GroupName = "Search Product Description")]
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase {

        private IProductService _productService;

        private IMapper _mapper;
        private readonly IConfiguration _configuration;  

        private readonly IWebHostEnvironment _env;

        private readonly ILogger<SearchController> _logger;

        public SearchController(
            IConfiguration configuration,
            IWebHostEnvironment env,
            IProductService productService,
            IMapper mapper,
            ILogger<SearchController> logger
            )
        {
            _configuration = configuration;  
            _productService = productService;
            _mapper = mapper;
            _logger = logger;
            _env = env;        
        }  

        [HttpPost("/api/searchproducts")]
        public IActionResult SearchProducts(ProductSearch prod) {
            try {                
                var products = _productService.SearchAll(prod.Search);
                if (products != null) {
                    var model = _mapper.Map<IList<ProductModel>>(products);
                    return Ok(new {products=model});
                } else {
                    return Ok(new {statuscode=404, message="No Data found."});
                }
            } catch(AppException ex) {
               return Ok(new {statuscode = 404, Message = ex.Message});
            }
        }
    }    
}