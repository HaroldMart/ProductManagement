using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Core.Application.ViewModels.Product
{
    public class SaveCategoryViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public int BusinessId { get; set; }
    }
}
