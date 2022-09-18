using System.Xml.Linq;

namespace SearchCore.Model
{
    public class Group : ISearchableData
    {
        private const int _nameWeight = 9;
        private const int _descriptionWeight = 5;

        private const int _nameWeightForMedium = 8;
        private const int _descriptionWeightForMedium = 0;


        private const int _exactMatchFactor = 10;

        public void CalculateWeight(string key)
        {
            var weight = 0;
            NameMatchWeightInMedium = 0;
            DescriptionMatchWeightInMedium = 0;

            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _nameWeight * _exactMatchFactor;
                    NameMatchWeightInMedium = _nameWeightForMedium * _exactMatchFactor;
                }
                else if (Name.Contains(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _nameWeight;
                    NameMatchWeightInMedium = _nameWeightForMedium;
                }
            }
            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key, StringComparison.OrdinalIgnoreCase)) {
                    weight += _descriptionWeight * _exactMatchFactor;
                    DescriptionMatchWeightInMedium = _descriptionWeightForMedium * _exactMatchFactor;
                }
                else if (Description.Contains(key, StringComparison.OrdinalIgnoreCase)) { 
                    weight += _descriptionWeight;
                    DescriptionMatchWeightInMedium = _descriptionWeightForMedium;
                }
            }

            CurrentWeight = weight;
        }

        public string ToDescriptionSummary()
        {
            return Description ?? string.Empty;
        }
        internal int NameMatchWeightInMedium { get; set; }
        internal int DescriptionMatchWeightInMedium { get; set; }
        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CurrentWeight { get; set; }
    }
}
