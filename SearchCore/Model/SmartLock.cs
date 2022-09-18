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
        public void CalculateWeight(string key)
        {
            var weight = 0;

            if (Type.ToString().Equals(key)) { weight += _typeWeight * 10; }
            else if (Type.ToString().Contains(key)) { weight += _typeWeight; }

            if (!string.IsNullOrEmpty(Name))
            {
                if (Name.Equals(key)) { weight += _nameWeight * 10; }
                else if (Name.Contains(key)) { weight += _nameWeight; }
            }

            if (!string.IsNullOrEmpty(SerialNumber))
            {
                if (SerialNumber.Equals(key)) { weight += _snWeight * 10; }
                else if (SerialNumber.Contains(key)) { weight += _snWeight; }
            }

            if (!string.IsNullOrEmpty(Floor))
            {
                if (Floor.Equals(key)) { weight += _floorWeight * 10; }
                else if (Floor.Contains(key)) { weight += _floorWeight; }
            }

            if (!string.IsNullOrEmpty(RoomNumber))
            {
                if (RoomNumber.Equals(key)) { weight += _roomWeight * 10; }
                else if (RoomNumber.Contains(key)) { weight += _roomWeight; }
            }

            if (!string.IsNullOrEmpty(Description))
            {
                if (Description.Equals(key)) { weight += _descriptionWeight * 10; }
                else if (Description.Contains(key)) { weight += _descriptionWeight; }
            }


            CurrentWeight = weight;

        }

        public string ToDescriptionSummary()
        {
            return $"{SerialNumber ?? string.Empty} | {Floor ?? string.Empty} | {RoomNumber ?? string.Empty} | {Description ?? string.Empty} | {BuildingName ?? string.Empty}";
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
