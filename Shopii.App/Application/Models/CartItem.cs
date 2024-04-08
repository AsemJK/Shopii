using Spark.Library.Database;

namespace Shopii.App.Application.Models
{
    public class CartItem : BaseModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }

        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public int UserId { get; set; }
        public virtual Product Product { get; set; }
    }
}
