using System.Text;

namespace SearchCore.Model
{
    public enum LockType
    {
        Cylinder,
        SmartHandle
    }
    public class SmartLock : ISearchableData
    {
        private const int _nameWeight = 10;
        private const int _typeWeight = 3;
        private const int _snWeight = 8;
        private const int _floorWeight = 6;
        private const int _roomWeight = 6;
        private const int _descriptionWeight = 6;


        private const int _exactMatchFactor = 10;
        public void CalculateWeight(string key)
        {
            var weight = 0;

            if (Type.ToString().Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _typeWeight * _exactMatchFactor; }
            else if (Type.ToString().Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _typeWeight; }

            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _nameWeight * _exactMatchFactor; }
                else if (Name.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _nameWeight; }
            }

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                if (SerialNumber.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _snWeight * _exactMatchFactor; }
                else if (SerialNumber.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _snWeight; }
            }

            if (!string.IsNullOrEmpty(Floor))
            {
                if (Floor.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _floorWeight * _exactMatchFactor; }
                else if (Floor.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _floorWeight; }
            }

            if (!string.IsNullOrEmpty(RoomNumber))
            {
                if (RoomNumber.Equals(key, StringComparison.OrdinalIgnoreCase)) { weight += _roomWeight * _exactMatchFactor; }
                else if (RoomNumber.Contains(key, StringComparison.OrdinalIgnoreCase)) { weight += _roomWeight; }
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
            return $"SerialNumber - {SerialNumber ?? string.Empty} |  Lock Type - {Type.ToString() ?? string.Empty} | Building - {BuildingName ?? string.Empty} | Floor - {Floor ?? string.Empty} | RoomNumber - {RoomNumber ?? string.Empty} | Description -{Description ?? string.Empty}";
        }

        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public LockType Type { get; set; }
        public string? Name { get; set; }
        public string? SerialNumber { get; set; }
        public string? Floor { get; set; }
        public string? RoomNumber { get; set; }
        public string? Description { get; set; }
        public int CurrentWeight { get; set; }
        internal string? BuildingName { get; set; }
    }
}
