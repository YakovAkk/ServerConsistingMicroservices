namespace ShopMicroservices.UrlStorage
{
    public class MyUrlStorage
    {
        public readonly string CategoryApiUrl = "https://localhost:7264/api/Category";

        private static MyUrlStorage instance;

        private MyUrlStorage()
        { }

        public static MyUrlStorage getInstance()
        {
            if (instance == null)
                instance = new MyUrlStorage();
            return instance;
        }
    }
}
