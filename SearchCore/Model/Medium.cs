using System.Text;

namespace SearchCore.Model
{
    public enum MediumType
    {
        Card,
        Transponder,
        TransponderWithCardInlay,
    }

    public class Medium: ISearchableData
    {
        private const int _typeWeight = 3;
        private const int _ownerWeight = 10;
        private const int _sRWeight = 8;
        private const int _descriptionWeight = 6;


        private const int _exactMatchFactor = 10;

        public void CalculateWeight(string key)
        {
            var weight = 0;

            if (Type.ToString().Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _typeWeight * _exactMatchFactor; }
            else if (Type.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _typeWeight; }

            if (!string.IsNullOrEmpty(Owner))
            {
                if (Owner.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _ownerWeight * _exactMatchFactor; }
                else if (Owner.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _ownerWeight; }
            }


            if (!string.IsNullOrEmpty(SerialNumber))
            {
                if (SerialNumber.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _sRWeight * _exactMatchFactor; }
                else if (SerialNumber.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _sRWeight; }
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _descriptionWeight * _exactMatchFactor; }
                else if (Description.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _descriptionWeight; }
            }

            CurrentWeight = weight;
        }

        public string ToDescriptionSummary()
        {
            return $"SerialNumber - {SerialNumber ?? string.Empty} | Medium Type - {Type.ToString() ?? string.Empty} | Group - {GroupName ?? string.Empty} | Description - {Description ?? string.Empty} ";
        }
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public MediumType Type { get; set; }
        public string? Owner { get; set; }
        public string? SerialNumber { get; set; }
        public string? Description { get; set; }
        public int CurrentWeight { get; set; }
        internal string? GroupName { get; set; }
    }
}
