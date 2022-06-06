namespace BasketData.Attibutes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class NameCollectionAttribute : Attribute
    {
        public string CollectionName { get; set; }
        public NameCollectionAttribute(string name)
        {
            CollectionName = name;
        }
    }
}
