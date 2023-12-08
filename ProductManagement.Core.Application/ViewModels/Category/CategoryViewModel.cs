using ProductManagement.Core.Application.ViewModels.Product;

namespace ProductManagement.Core.Application.ViewModels.Category
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int ProductsCount { get; set; }
        public ICollection<ProductViewModel>? Products { get; set; }
    }
}
