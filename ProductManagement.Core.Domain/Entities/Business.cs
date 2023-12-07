using ProductManagement.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Domain.Entities
{
    public class Business
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
        public ICollection<Category>? Categories { get; set; }
        public string IdUser { get; set; }
    }
}
