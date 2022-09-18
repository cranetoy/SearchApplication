namespace SearchCore.Model
{
    public class SearchableEntityDTO
    {
        public string Name { get; set; }
        public string EntityType { get; set; }
        public string Description { get; set; }

        public SearchableEntityDTO(string entityType, string name,  string description)
        {
            Name = name;
            Description = description;
            EntityType = entityType;
        }
    }
}
