using System.Xml.Linq;

namespace SearchCore.Model
{
    public class Group : IData
    {
        private const int _nameWeight = 9;
        private const int _descriptionWeight = 5;
        private const int _nameWeightForMedium = 8;
        private int _exactMatchFactor = 10;

        public void CalculateWeight(string key)
        {
            var weight = 0;
            NameMatchWeightInMedium = 0;
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key))
                {
                    weight += _nameWeight * _exactMatchFactor;
                    NameMatchWeightInMedium = _nameWeightForMedium * _exactMatchFactor;
                }
                else if (Name.Contains(key))
                {
                    weight += _nameWeight;
                    NameMatchWeightInMedium = _nameWeightForMedium;
                }
            }
            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key)) { weight += _descriptionWeight * _exactMatchFactor; }
                else if (Description.Contains(key)) { weight += _descriptionWeight; }
            }

            CurrentWeight = weight;
        }
        internal int NameMatchWeightInMedium { get; set; }
        
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CurrentWeight { get; set; }
    }
}
