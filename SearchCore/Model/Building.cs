using System.Xml.Linq;

namespace SearchCore.Model
{
    public class Building : IData
    {
        private const int _shortcutWeight = 9;
        private const int _nameWeight = 9;
        private const int _descriptionWeight = 5;

        private const int _nameWeightForLock = 8;
        private const int _shortcutWeightForLock = 5;
        private int _exactMatchFactor = 10;

        internal int NameMatchWeightInLock { get; set; }
        internal int ShortcutMatchWeightInLock { get; set; }

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

            if (!string.IsNullOrEmpty(Shortcut))
            {
                if (Shortcut.Equals(key))
                {
                    weight += _shortcutWeight * _exactMatchFactor;
                    ShortcutMatchWeightInLock = _shortcutWeightForLock * _exactMatchFactor;
                }
                else if (Shortcut.Contains(key))
                {
                    weight += _shortcutWeight;
                    ShortcutMatchWeightInLock = _shortcutWeightForLock;
                }
            }
            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key))
                {
                    weight += _nameWeight * _exactMatchFactor;
                    NameMatchWeightInLock = _nameWeightForLock * _exactMatchFactor;
                }
                else if (Name.Contains(key))
                {
                    weight += _nameWeight;
                    NameMatchWeightInLock = _nameWeightForLock;
                }
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key)) { weight += _descriptionWeight * 10; }
                else if (Description.Contains(key)) { weight += _descriptionWeight; }
            }

            CurrentWeight = weight;
        }
    }
}
