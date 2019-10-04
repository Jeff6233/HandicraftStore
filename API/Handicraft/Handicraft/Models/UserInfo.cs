using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class UserInfo : _DbBase
    {
        public UserInfo()
        {
            UserCollect = new HashSet<UserCollect>();
            ShoppingCar = new HashSet<ShoppingCar>();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OpenId { get; set; }
        public string Address { get; set; }
        public string City { get; set; }

        [JsonIgnore]
        public virtual ICollection<ShoppingCar> ShoppingCar { get; set; }
        [JsonIgnore]
        public virtual ICollection<UserCollect> UserCollect { get; set; }
    }
}
