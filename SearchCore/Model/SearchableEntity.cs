namespace SearchCore.Model
{
    public abstract class SearchableEntity
    {
        protected int _matchFactor = 10;
        protected int CurrentWeight { get; set; }
        internal abstract void CalculateWeight(string key);       

    }
}