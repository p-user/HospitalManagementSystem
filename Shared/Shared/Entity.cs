using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public abstract class Entity<T> : IEntity<T>
    {
        public T Id { get; set; }
        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
