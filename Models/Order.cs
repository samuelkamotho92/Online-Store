using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Store.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [ForeignKey("productId")]

        public Products Products { get; set; }

        public Guid productId { get; set; }
    }
}
