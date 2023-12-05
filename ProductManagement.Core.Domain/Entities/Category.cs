using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Core.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int BusinessId { get; set; }
        public Business Business  { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
