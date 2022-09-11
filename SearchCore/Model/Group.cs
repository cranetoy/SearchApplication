namespace SearchCore.Model
{
    public class Group : SearchableEntity
    {
        private const int _nameWeight = 9;
        private const int _descriptionWeight = 5;
        private const int _nameWeightForMedium = 8;

        internal int NameMatchWeightInMedium { get; set; }
        internal override void CalculateWeight(string key)
        {
            var weight = 0;
            NameMatchWeightInMedium = 0;
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key))
                {
                    weight += _nameWeight * _matchFactor;
                    NameMatchWeightInMedium = _nameWeightForMedium * _matchFactor;
                }
                else if (Name.Contains(key))
                {
                    weight += _nameWeight;
                    NameMatchWeightInMedium = _nameWeightForMedium;
                }
            }
            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key)) { weight += _descriptionWeight * _matchFactor; }
                else if (Description.Contains(key)) { weight += _descriptionWeight; }
            }

            CurrentWeight = weight;
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}
