﻿using Spark.Library.Database;

namespace Shopii.App.Application.Models
{
    public class Product : BaseModel
    {
        public Product()
        {
            CartItems = new HashSet<CartItem>();
        }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<CartItem> CartItems { get; set; }
    }
}
