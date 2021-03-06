using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using projectRootNamespace.Api.Services;
using projectRootNamespace.Api.Infrastructure;
using Swashbuckle.AspNetCore.Annotations;

namespace projectRootNamespace.Api.Controllers
{
    [Route("/products")]
    [SwaggerTag("Products")]
    public class ProductsController : BaseController
    {
        private readonly IProductsService _svc;

        public ProductsController(IProductsService svc) =>
            _svc = svc;

        /// <summary>
        /// Get the list of products.
        /// </summary>
        /// <param name="searchTerm">(Optional) Get records that match the specified value.</param>
        /// <param name="page">(Optional) The page of records to retrieve.</param>
        /// <param name="pageSize">(Optional) The number of records per page to retrieve.</param>
        /// <returns>The list of records found.</returns>
        /// <response code="200">Return the list of records found.</response>
        /// <response code="400">Bad request.</response>
        [HttpGet]
        public Task<PagedResultsDTO<ProductListDTO>> FindMany([FromQuery] string searchTerm, [FromQuery] int page = 0, [FromQuery] int pageSize = 0) =>
             _svc.FindMany(searchTerm, page, pageSize);

        /// <summary>
        /// Get the specified product.
        /// </summary>
        /// <param name="productId">The record unique identifier.</param>
        /// <returns>The record if found.</returns>
        /// <response code="200">Return the record found.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpGet("{productId}")]
        public Task<ProductDTO> Find(int productId) =>
             _svc.Find(productId);

        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="dto">The record creation dto.</param>
        /// <returns>The record that was created</returns>
        /// <response code="201">Return the record created.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        public Task<ProductDTO> Create(ProductCreateDTO dto) =>
             _svc.Create(dto);

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="productId">The record unique identifier.</param>
        /// <param name="dto">The record update dto.</param>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpPut("{productId}")]
        public Task Update(int productId, ProductUpdateDTO dto) =>
             _svc.Update(productId, dto);

        /// <summary>
        /// Deletes an existing product.
        /// </summary>
        /// <param name="productId">The record unique identifier.</param>
        /// <response code="204">Operation was successful.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Record not found.</response>
        [HttpDelete("{productId}")]
        public Task Delete(int productId) =>
             _svc.Delete(productId);
    }
}
