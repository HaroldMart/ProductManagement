using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Core.Application.ViewModels.Business
{
    public class SaveBusinessViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public string IdUser { get; set; }
    }
}
