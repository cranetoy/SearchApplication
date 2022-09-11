using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchCore.Model
{
    public enum MediumType
    {
        Card,
        Transponder,
        TransponderWithCardInlay,
    }
    public class Medium : SearchableEntity
    {
        internal override void CalculateWeight(string key)
        {
            throw new NotImplementedException();
        }

        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public MediumType Type { get; set; }
        public string? Owner { get; set; }
        public string? SerialNumber { get; set; }
        public string? Description { get; set; }
    }
}
