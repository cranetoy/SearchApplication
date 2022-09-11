namespace SearchCore.Model
{
    public abstract class SearchableEntity
    {
        protected const int _matchFactor = 10;
        internal int CurrentWeight { get; set; }
        internal abstract void CalculateWeight(string key);       

    }
}