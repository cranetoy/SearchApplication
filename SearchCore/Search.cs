using SearchCore.Model;
using System.Collections.Generic;

namespace SearchCore
{
    public class Search
    {
        public Search(SearchDataSet dataSet)
        {
            _dataSet = dataSet;
        }

        private SearchDataSet _dataSet;

        public IEnumerable<SearchableEntityDTO> GetResults(string searchKey)
        {
            var results = new Dictionary<SearchableEntity, int>();

            //Calculate weight of each Building for given SearchKey
            foreach (var entity in _dataSet.Buildings)
            {
                entity.CalculateWeight(searchKey);
            }
            //Calculate weight of each SmartLock for given SearchKey
            foreach (var entity in _dataSet.Locks)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Group for given SearchKey
            foreach (var entity in _dataSet.Groups)
            {
                entity.CalculateWeight(searchKey);
            }

            //Calculate weight of each Medium for given SearchKey
            foreach (var entity in _dataSet.Media)
            {
                entity.CalculateWeight(searchKey);
            }


            //Update current weight of each Lock for transitive fields
            foreach (var entity in _dataSet.Buildings.Where(b => b.CurrentWeight > 0))
            {
                var locksInBuilding = _dataSet.Locks.Where(x => x.BuildingId.Equals(entity.Id)).ToList();

                // Add Building with current weight larger than zero to result
                var key = new SearchableEntity<Building>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);

                foreach (var smartLock in locksInBuilding)
                {
                    smartLock.BuildingName = entity.Name;
                    smartLock.CurrentWeight += (entity.NameMatchWeightInLock + entity.ShortcutMatchWeightInLock);
                }
            }


            //Update current weight of each Medium for transitive fields
            foreach (var entity in _dataSet.Groups.Where(b => b.CurrentWeight > 0))
            {
                var mediumInGroup = _dataSet.Media.Where(x => x.GroupId.Equals(entity.Id)).ToList();

                // Add Group with current weight larger than zero to result
                var key = new SearchableEntity<Group>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);

                foreach (var medium in mediumInGroup)
                {
                    medium.GroupName = entity.Name; 
                    medium.CurrentWeight += entity.NameMatchWeightInMedium;
                }
            }

            // Add SmartLock with current weight larger than zero to result 
            foreach (var entity in _dataSet.Locks.Where(b => b.CurrentWeight > 0))
            {
                var key = new SearchableEntity<SmartLock>();
                key.Data = entity;
                results.Add(key, entity.CurrentWeight);
            }

            // Add Medium with current weight larger than zero to result
            foreach (var entity in _dataSet.Media.Where(b => b.CurrentWeight > 0))
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
                return new SearchableEntityDTO(building.Data.Name ?? string.Empty,
                    building.Data.ToDescriptionSummary());
            }
            else if (item is SearchableEntity<SmartLock> smartLock)
            {
                return new SearchableEntityDTO(smartLock.Data.Name ?? string.Empty,
                    smartLock.Data.ToDescriptionSummary());                
            }
            else if (item is SearchableEntity<Group> group)
            {
                return new SearchableEntityDTO(group.Data.Name ?? string.Empty,
                    group.Data.ToDescriptionSummary());
            }
            else if (item is SearchableEntity<Medium> medium)
            {
                return new SearchableEntityDTO(medium.Data.Owner ?? string.Empty,
                    medium.Data.ToDescriptionSummary());
            }
            else return new SearchableEntityDTO(string.Empty, string.Empty);
        }
    }
}
