using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCore.Model
{
    public enum LockType
    {
        Cylinder,
        SmartHandle
    }
    public class SmartLock : SearchableEntity
    {
        internal override void CalculateWeight(string key)
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public LockType Type { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? Floor { get; set; }
        public string? RoomNumber { get; set; }
        public string? Description { get; set; }
    }
}
