using SearchCore.Model;

namespace SearchCore
{
    public class Search
    {
        public Search(SearchDataSet data)
        {
            _data = data;
        }

        private SearchDataSet _data;

        public IEnumerable<SearchableEntity> GetResults(string searchKey)
        {
            var results = new Dictionary<SearchableEntity, int>();

            //Calculate weight of each Building for given SearchKey
            foreach (var entity in _data.Buildings)
            {
                entity.CalculateWeight(searchKey);
            }
            //Calculate weight of each SmartLock for given SearchKey
            foreach (var entity in _data.Locks)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Group for given SearchKey
            foreach (var entity in _data.Groups)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Medium for given SearchKey
            foreach (var entity in _data.Media)
            {
                entity.CalculateWeight(searchKey);
            }


            //Update current weight of each Lock for transitive fields
            foreach (var entity in _data.Buildings.Where(b => b.CurrentWeight > 0))
            {
                var locksInBuilding = _data.Locks.Where(x => x.BuildingId.Equals(entity.Id)).ToList();
                
                // Add Building with current weight larger than zero to result
                results.Add(entity, entity.CurrentWeight);

                foreach (var smartLock in locksInBuilding)
                {
                    smartLock.CurrentWeight += (entity.NameMatchWeightInLock + entity.ShortcutMatchWeightInLock);
                }
            }


            //Update current weight of each Medium for transitive fields
            foreach (var entity in _data.Groups.Where(b => b.CurrentWeight > 0))
            {
                var mediumInGroup = _data.Media.Where(x => x.GroupId.Equals(entity.Id)).ToList();

                // Add Group with current weight larger than zero to result
                results.Add(entity, entity.CurrentWeight);

                foreach (var medium in mediumInGroup)
                {
                    medium.CurrentWeight += entity.NameMatchWeightInMedium;
                }
            }

            // Add SmartLock with current weight larger than zero to result 
            foreach (var entity in _data.Locks.Where(b => b.CurrentWeight > 0))
            {
                results.Add(entity, entity.CurrentWeight);
            }

            // Add Medium with current weight larger than zero to result
            foreach (var entity in _data.Media.Where(b => b.CurrentWeight > 0))
            {
                results.Add(entity, entity.CurrentWeight);
            }

            var sortedResult = results.OrderByDescending(pair => pair.Value).ToDictionary(x=>x.Key, x=> x.Value);

            return sortedResult.Keys.ToList() ?? new List<SearchableEntity>();
        } 
    }
}
