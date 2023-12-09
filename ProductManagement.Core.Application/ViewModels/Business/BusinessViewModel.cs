using ProductManagement.Core.Application.ViewModels.Category;

namespace ProductManagement.Core.Application.ViewModels.Business
{
    public class BusinessViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int? CategoriesCount { get; set; }
        public ICollection<CategoryViewModel>? Categories { get; set; }
    }
}
