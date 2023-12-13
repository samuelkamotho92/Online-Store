namespace Online_Store.Models
{
    public class Products
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public int price { get; set; }
    }
}
