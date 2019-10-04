using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class Product:_DbBase
    {
        public Product()
        {
            ProductImage = new HashSet<ProductImage>();
        }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Introduction { get; set; }
        public string Tips { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}
