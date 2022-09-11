using SearchCore.Model;

namespace SearchCore
{
    public class SearchDataSet
    {
        public IEnumerable<Building> Buildings { get; set; }
        public IEnumerable<SmartLock> Locks { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<Medium> Media { get; set; }
    }
}
