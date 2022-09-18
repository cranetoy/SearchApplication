using System.Text;
using System.Xml.Linq;

namespace SearchCore.Model
{
    public class Building : ISearchableData
    {
        private const int _shortcutWeight = 7;
        private const int _nameWeight = 9;
        private const int _descriptionWeight = 5;

        private const int _nameWeightForLock = 8;
        private const int _shortcutWeightForLock = 5;
        private const int _descriptionWeightForLock = 0;


        private const int _exactMatchFactor = 10;

        internal int NameMatchWeightInLock { get; set; }
        internal int ShortcutMatchWeightInLock { get; set; }
        internal int DescriptionMatchWeightInLock { get; set; }

        public Guid Id { get; set; }
        public string? Shortcut { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int CurrentWeight { get; set; }

        public void CalculateWeight(string key)
        {
            var weight = 0;
            NameMatchWeightInLock = 0;
            ShortcutMatchWeightInLock = 0;
            DescriptionMatchWeightInLock = 0;

            if (!string.IsNullOrEmpty(Shortcut))
            {
                if (Shortcut.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _shortcutWeight * _exactMatchFactor;
                    ShortcutMatchWeightInLock = _shortcutWeightForLock * _exactMatchFactor;
                }
                else if (Shortcut.Contains(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _shortcutWeight;
                    ShortcutMatchWeightInLock = _shortcutWeightForLock;
                }
            }
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _nameWeight * _exactMatchFactor;
                    NameMatchWeightInLock = _nameWeightForLock * _exactMatchFactor;
                }
                else if (Name.Contains(key, StringComparison.OrdinalIgnoreCase))
                {
                    weight += _nameWeight;
                    NameMatchWeightInLock = _nameWeightForLock;
                }
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key, StringComparison.OrdinalIgnoreCase)) {
                    weight += _descriptionWeight * _exactMatchFactor;
                    DescriptionMatchWeightInLock = _descriptionWeightForLock * _exactMatchFactor;
                }
                else if (Description.Contains(key, StringComparison.OrdinalIgnoreCase)) { 
                    weight += _descriptionWeight;
                    DescriptionMatchWeightInLock = _descriptionWeightForLock;
                }
            }

            CurrentWeight = weight;
        }

        public string ToDescriptionSummary()
        {
            return $"Shortcut - {Shortcut ?? string.Empty} | Description - {Description ?? string.Empty}";
            
        }
    }
}
