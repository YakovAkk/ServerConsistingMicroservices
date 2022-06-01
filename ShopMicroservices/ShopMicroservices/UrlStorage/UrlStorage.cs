namespace ShopMicroservices.UrlStorage
{
    public class MyUrlStorage
    {
        public readonly string CategoryApiUrl = "https://localhost:7264/api/Category";
        public readonly string AccountApiUrl = "https://localhost:7224/api/Account";

        private static MyUrlStorage Instance;

        private MyUrlStorage()
        { }

        public static MyUrlStorage getInstance()
        {
            if (Instance == null)
                Instance = new MyUrlStorage();
            return Instance;
        }
    }
}
