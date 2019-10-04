using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class ProductImage:_DbBase
    {
        public ProductImage()
        {
            BannerImage = new HashSet<BannerImage>();
            DetailImage = new HashSet<DetailImage>();
        }
        public Guid ProductId { get; set; }
        
        [JsonIgnore]
        public virtual Product Product { get; set; }
        [JsonIgnore]
        public virtual ICollection<BannerImage> BannerImage { get; set; }
        [JsonIgnore]
        public virtual ICollection<DetailImage> DetailImage { get; set; }
    }
}
