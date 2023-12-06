using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Core.Application.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
