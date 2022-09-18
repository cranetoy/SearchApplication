namespace SearchCore.Model
{
    public class SearchableEntityDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public SearchableEntityDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
