using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Infinite.API.EComAPI.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }      
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}