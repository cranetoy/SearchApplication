using SearchCore.Model;

namespace SearchCore
{
    public class Search
    {

        public IEnumerable<SearchableEntityDTO> GetSearchResults(string searchKey, SearchDataSet dataSet)
        {
            if(dataSet == null || string.IsNullOrEmpty(searchKey)) return new List<SearchableEntityDTO>();

            var results = new Dictionary<SearchableEntity, int>();
            //Calculate weight of each Building for given SearchKey
            foreach (var entity in dataSet.Buildings)
            {
                entity.CalculateWeight(searchKey);
            }
            //Calculate weight of each SmartLock for given SearchKey
            foreach (var entity in dataSet.Locks)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Group for given SearchKey
            foreach (var entity in dataSet.Groups)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Medium for given SearchKey
            foreach (var entity in dataSet.Media)
            {
                entity.CalculateWeight(searchKey);
            }


            //Update current weight of each Lock for transitive fields
            foreach (var entity in dataSet.Buildings.Where(b => b.CurrentWeight > 0))
            {
                var locksInBuilding = dataSet.Locks.Where(x => x.BuildingId.Equals(entity.Id)).ToList();

                // Add Building with current weight larger than zero to result
                var key = new SearchableEntity<Building>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);

                foreach (var smartLock in locksInBuilding)
                {
                    smartLock.BuildingName = entity.Name;
                    smartLock.CurrentWeight += (entity.NameMatchWeightInLock + entity.ShortcutMatchWeightInLock + entity.DescriptionMatchWeightInLock);
                }
            }


            //Update current weight of each Medium for transitive fields
            foreach (var entity in dataSet.Groups.Where(b => b.CurrentWeight > 0))
            {
                var mediumInGroup = dataSet.Media.Where(x => x.GroupId.Equals(entity.Id)).ToList();

                // Add Group with current weight larger than zero to result
                var key = new SearchableEntity<Group>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);

                foreach (var medium in mediumInGroup)
                {
                    medium.GroupName = entity.Name; 
                    medium.CurrentWeight += (entity.NameMatchWeightInMedium + entity.DescriptionMatchWeightInMedium);
                }
            }

            // Add SmartLock with current weight larger than zero to result 
            foreach (var entity in dataSet.Locks.Where(b => b.CurrentWeight > 0))
            {
                var key = new SearchableEntity<SmartLock>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);
            }

            // Add Medium with current weight larger than zero to result
            foreach (var entity in dataSet.Media.Where(b => b.CurrentWeight > 0))
            {
                var key = new SearchableEntity<Medium>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);
            }

            var sortedResult = results.OrderByDescending(pair => pair.Value)
                .ToDictionary(x=>x.Key, x=> x.Value)
                .Select(pair =>
                {
                    return GetSearchableEntityDTO(pair.Key);
                }).Where(d => !string.IsNullOrEmpty(d.Name));

            return sortedResult;
        }

        private SearchableEntityDTO GetSearchableEntityDTO(SearchableEntity item)
        {
            if (item is SearchableEntity<Building> building)
            {
                return new SearchableEntityDTO("Building",  building.Data.Name ?? string.Empty, 
                    building.Data.ToDescriptionSummary());
            }
            else if (item is SearchableEntity<SmartLock> smartLock)
            {
                return new SearchableEntityDTO("SmartLock", smartLock.Data.Name ?? string.Empty,
                    smartLock.Data.ToDescriptionSummary());                
            }
            else if (item is SearchableEntity<Group> group)
            {
                return new SearchableEntityDTO("Group", group.Data.Name ?? string.Empty,
                    group.Data.ToDescriptionSummary());
            }
            else if (item is SearchableEntity<Medium> medium)
            {
                return new SearchableEntityDTO("Medium", medium.Data.Owner ?? string.Empty,
                    medium.Data.ToDescriptionSummary());
            }
            else return new SearchableEntityDTO(string.Empty, string.Empty, string.Empty);
        }
    }
}
