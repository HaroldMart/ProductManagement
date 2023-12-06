using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Core.Application.ViewModels
{
    public class SaveProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public int CategoryId { get; set; }
    }
}
