namespace ProductManagement.Core.Application.ViewModels.Product
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public string CategoryName { get; set; }
    }
}
