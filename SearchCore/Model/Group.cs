using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCore.Model
{
    public class Group : SearchableEntity
    {
        internal override void CalculateWeight(string key)
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
