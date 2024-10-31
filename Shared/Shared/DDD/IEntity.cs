using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DDD
{
    public interface IEntity
    {

        public string? LastUpdatedBy { get; set; }
        public DateTime? LastUpdate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
    }


    public interface IEntity<T> : IEntity
    {
        public T Id { get; set; }
    }
}
