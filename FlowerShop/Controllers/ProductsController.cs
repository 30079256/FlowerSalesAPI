using FlowerSales.API.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace FlowerSales.API.Controllers
{
    [ApiVersion("1.0")]
    [Route("products")]
    [ApiController]
    public class ProductsV1Controller : ControllerBase
    {
        private readonly ShopContext _shopContext;

        public ProductsV1Controller(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery] ProductParametersQuery queryParameters)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>();

            // Combine filters for Name, CategoryName, StoreLocation using OR
            var orFilters = new List<FilterDefinition<Product>>();
            if (!string.IsNullOrEmpty(queryParameters.Name))
                orFilters.Add(filterBuilder.Regex(x => x.Name, new BsonRegularExpression(queryParameters.Name, "i")));
            if (!string.IsNullOrEmpty(queryParameters.CategoryName))
                orFilters.Add(filterBuilder.Regex(x => x.CategoryName, new BsonRegularExpression(queryParameters.CategoryName, "i")));
            if (!string.IsNullOrEmpty(queryParameters.StoreLocation))
                orFilters.Add(filterBuilder.Regex(x => x.StoreLocation, new BsonRegularExpression(queryParameters.StoreLocation, "i")));

            if (orFilters.Count > 0)
                filters.Add(orFilters.Count == 1 ? orFilters.First() : filterBuilder.Or(orFilters));

            // SearchTerm logic
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                var searchTermPattern = $".*{queryParameters.SearchTerm}.*";
                filters.Add(filterBuilder.Or(
                    filterBuilder.Regex(x => x.Name, new BsonRegularExpression(searchTermPattern, "i")),
                    filterBuilder.Regex(x => x.CategoryName, new BsonRegularExpression(searchTermPattern, "i")),
                    filterBuilder.Regex(x => x.StoreLocation, new BsonRegularExpression(searchTermPattern, "i"))
                ));
            }

            // Price filters
            if (queryParameters.MinPrice.HasValue)
                filters.Add(filterBuilder.Gte(x => x.Price, queryParameters.MinPrice.Value));
            if (queryParameters.MaxPrice.HasValue)
                filters.Add(filterBuilder.Lte(x => x.Price, queryParameters.MaxPrice.Value));

            var finalFilter = filters.Count > 0 ? filterBuilder.And(filters) : filterBuilder.Empty;

            // Sorting
            var sort = queryParameters.SortOrder == "desc"
                ? Builders<Product>.Sort.Descending(queryParameters.SortBy)
                : Builders<Product>.Sort.Ascending(queryParameters.SortBy);

            var products = await _shopContext.Products.Find(finalFilter)
                .Sort(sort)
                .Skip((queryParameters.Page - 1) * queryParameters.Size)
                .Limit(queryParameters.Size)
                .ToListAsync();

            if (products.Count == 0)
                return NotFound();

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _shopContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Id = ModelBuilderExtension.GenerateUniqueRandomIdForNewProduct(_shopContext);

            await _shopContext.Products.InsertOneAsync(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            product.Id = id;
            var result = await _shopContext.Products.ReplaceOneAsync(x => x.Id == id, product);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            var updatedProduct = await _shopContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _shopContext.Products.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            var result = await _shopContext.Products.DeleteManyAsync(Builders<Product>.Filter.Empty);

            if (result.DeletedCount == 0)
            {
                return NotFound("No products found to delete.");
            }

            return NoContent();
        }
    }


    [ApiVersion("2.0")]
    [Route("products")]
    [ApiController]
    public class ProductsV2Controller : ControllerBase
    {
        private readonly ShopContext _shopContext;

        public ProductsV2Controller(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts([FromQuery] ProductParametersQuery queryParameters)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>
            {
                // Filter for IsAvailable = true
                filterBuilder.Eq(x => x.IsAvailable, true)
            };

            // Combine filters for Name, CategoryName, StoreLocation using OR
            var orFilters = new List<FilterDefinition<Product>>();
            if (!string.IsNullOrEmpty(queryParameters.Name))
                orFilters.Add(filterBuilder.Regex(x => x.Name, new BsonRegularExpression(queryParameters.Name, "i")));
            if (!string.IsNullOrEmpty(queryParameters.CategoryName))
                orFilters.Add(filterBuilder.Regex(x => x.CategoryName, new BsonRegularExpression(queryParameters.CategoryName, "i")));
            if (!string.IsNullOrEmpty(queryParameters.StoreLocation))
                orFilters.Add(filterBuilder.Regex(x => x.StoreLocation, new BsonRegularExpression(queryParameters.StoreLocation, "i")));

            if (orFilters.Count > 0)
                filters.Add(orFilters.Count == 1 ? orFilters.First() : filterBuilder.Or(orFilters));

            // SearchTerm logic
            if (!string.IsNullOrEmpty(queryParameters.SearchTerm))
            {
                var searchTermPattern = $".*{queryParameters.SearchTerm}.*";
                filters.Add(filterBuilder.Or(
                    filterBuilder.Regex(x => x.Name, new BsonRegularExpression(searchTermPattern, "i")),
                    filterBuilder.Regex(x => x.CategoryName, new BsonRegularExpression(searchTermPattern, "i")),
                    filterBuilder.Regex(x => x.StoreLocation, new BsonRegularExpression(searchTermPattern, "i"))
                ));
            }

            // Price filters
            if (queryParameters.MinPrice.HasValue)
                filters.Add(filterBuilder.Gte(x => x.Price, queryParameters.MinPrice.Value));
            if (queryParameters.MaxPrice.HasValue)
                filters.Add(filterBuilder.Lte(x => x.Price, queryParameters.MaxPrice.Value));

            var finalFilter = filterBuilder.And(filters);

            // Sorting
            var sort = queryParameters.SortOrder == "desc"
                ? Builders<Product>.Sort.Descending(queryParameters.SortBy)
                : Builders<Product>.Sort.Ascending(queryParameters.SortBy);

            var products = await _shopContext.Products.Find(finalFilter)
                .Sort(sort)
                .Skip((queryParameters.Page - 1) * queryParameters.Size)
                .Limit(queryParameters.Size)
                .ToListAsync();

            if (products.Count == 0)
                return NotFound();

            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _shopContext.Products.Find(x => x.Id == id && x.IsAvailable == true).FirstOrDefaultAsync();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            product.Id = ModelBuilderExtension.GenerateUniqueRandomIdForNewProduct(_shopContext);

            await _shopContext.Products.InsertOneAsync(product);
            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product product)
        {
            product.Id = id;
            var result = await _shopContext.Products.ReplaceOneAsync(x => x.Id == id, product);
            if (result.ModifiedCount == 0)
            {
                return NotFound();
            }
            var updatedProduct = await _shopContext.Products.Find(x => x.Id == id).FirstOrDefaultAsync();
            return Ok(updatedProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _shopContext.Products.DeleteOneAsync(x => x.Id == id);
            if (result.DeletedCount == 0)
            {
                return NotFound();
            }
            return NoContent();
        }

        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteAllProducts()
        {
            var result = await _shopContext.Products.DeleteManyAsync(Builders<Product>.Filter.Empty);

            if (result.DeletedCount == 0)
            {
                return NotFound("No products found to delete.");
            }

            return NoContent();
        }
    }
}


