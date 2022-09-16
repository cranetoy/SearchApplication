namespace SearchCore.Model
{
    public class SearchableEntities
    {
        public List<dynamic> Items { get; set; }
    }
    public abstract class SearchableEntity
    {
    }

    public class SearchableEntity<T> : SearchableEntity where T : class
    {
        public T Data { get; set; }

    }

    public interface IData 
    {
        int CurrentWeight { get; set; }
        void CalculateWeight(string key); 
    }
            
}