using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Handicraft.Models
{
    public class UserCollect:_DbBase
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }

        public virtual UserInfo UserInfo { get; set; }
    }
}
