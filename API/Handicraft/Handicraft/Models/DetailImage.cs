using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class DetailImage : _DbBase
    {
        public Guid ProductImageId { get; set; }
        public string ImagePath { get; set; }

        [JsonIgnore]
        public virtual ProductImage ProductImage { get; set; }
    }
}
