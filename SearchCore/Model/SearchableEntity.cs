namespace SearchCore.Model
{
    public abstract class SearchableEntity
    {
    }

    public class SearchableEntity<T> : SearchableEntity where T : class
    {
        public T Data { get; set; }

    }

    public interface ISearchableData 
    {
        int CurrentWeight { get; set; }
        void CalculateWeight(string key);
        string ToDescriptionSummary();
    }
            
}