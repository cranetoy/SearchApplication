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
        private const int _typeWeight = 3;
        private const int _ownerWeight = 10;
        private const int _sRWeight = 8;
        private const int _descriptionWeight = 6;
        internal override void CalculateWeight(string key)
        {
            var weight = 0;

            if (Type.ToString().Equals(key)) { weight += _typeWeight * 10; }
            else if (Type.ToString().Contains(key)) { weight += _typeWeight; }

            if (!string.IsNullOrEmpty(Owner))
            {
                if (Owner.Equals(key)) { weight += _ownerWeight * 10; }
                else if (Owner.Contains(key)) { weight += _ownerWeight; }
            }


            if (!string.IsNullOrEmpty(SerialNumber))
            {
                if (SerialNumber.Equals(key)) { weight += _sRWeight * 10; }
                else if (SerialNumber.Contains(key)) { weight += _sRWeight; }
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key)) { weight += _descriptionWeight * 10; }
                else if (Description.Contains(key)) { weight += _descriptionWeight; }
            }

            CurrentWeight = weight;
        }

        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public MediumType Type { get; set; }
        public string? Owner { get; set; }
        public string? SerialNumber { get; set; }
        public string? Description { get; set; }
    }
}
