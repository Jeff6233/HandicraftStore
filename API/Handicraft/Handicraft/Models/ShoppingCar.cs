using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class ShoppingCar:_DbBase
    {
        public ShoppingCar()
        {
            Product = new HashSet<Product>();
        }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public decimal Amount { get; set; }

        [JsonIgnore]
        public virtual UserInfo UserInfo { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Product { get; set; }
    }
}
