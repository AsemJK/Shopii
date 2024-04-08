using Spark.Library.Database;

namespace Shopii.App.Application.Models
{
    public class Category : BaseModel
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }

        public string Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
