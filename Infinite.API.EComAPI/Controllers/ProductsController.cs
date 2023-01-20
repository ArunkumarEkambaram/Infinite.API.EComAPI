using Infinite.API.EComAPI.DTOs;
using Infinite.API.EComAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Infinite.API.EComAPI.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly EComDBEntities dbContext = null;

        public ProductsController()
        {
            dbContext = new EComDBEntities();
        }


        [HttpGet]
        [Route("GetAllProduct")]
        public List<ProductDto> GetProducts()
        {
            var products = dbContext.Products.Include(p => p.Category).Select(x => new ProductDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                Description = x.Description,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.CategoryName
            }).ToList();
            return products;
        }

        [HttpGet, Route("GetProductById/{id}", Name = "GetById")]
        //[Route("GetProductById/{id}")] //api/Products/GetProductById/1
        public async Task<IHttpActionResult> GetProductById(int id)
        {
            var product = await dbContext.Products.Select(x => new ProductDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                Description = x.Description,
                CategoryId = x.CategoryId
            }).FirstOrDefaultAsync(c => c.Id == id);


            if (product != null)
            {
                return Ok(product);//return 200
            }
            return NotFound();//return 404
        }


        [Route("ProductByPrice/{price}")] //api/Products/ProductByPrice/800000
        public async Task<IHttpActionResult> GetProductByPrice([FromUri] double price)
        {
            var product = await dbContext.Products.Select(x => new ProductDto
            {
                Id = x.Id,
                ProductName = x.ProductName,
                Price = x.Price,
                Quantity = x.Quantity,
                Description = x.Description,
                CategoryId = x.CategoryId
            }).Where(x => x.Price > price).ToListAsync();
            return Ok(product);
        }

        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IHttpActionResult> AddNewProduct([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            dbContext.Products.Add(product);
            await dbContext.SaveChangesAsync();
            return CreatedAtRoute("GetById", new { id = product.Id }, product);
            // return Created(new Uri(""), product);
        }
    }
}
