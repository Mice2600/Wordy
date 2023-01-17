namespace Base
{
    public partial interface IContent
    {
        public string EnglishSource { get; }
        public string RussianSource { get; }
        public float Score { get; }
        public bool Active { get; }
    }
}