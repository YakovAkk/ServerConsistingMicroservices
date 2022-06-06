using ShopMicroservices.Enum;

namespace ShopMicroservices.UrlStorage
{
    public class MyUrlStorage
    {
        public readonly Dictionary<UrlEnum, string> ApisUrl;

        private static MyUrlStorage Instance;

        private MyUrlStorage()
        {
            ApisUrl = new Dictionary<UrlEnum, string>();
            ApisUrl.Add(UrlEnum.AccountApiUrl, "https://localhost:7224/api/Account");
            ApisUrl.Add(UrlEnum.CategoryApiUrl, "https://localhost:7264/api/Category");
            ApisUrl.Add(UrlEnum.LegoApiUrl, "https://localhost:7249/api/Lego");
        }

        public static MyUrlStorage getInstance()
        {
            if (Instance == null)
                Instance = new MyUrlStorage();
            return Instance;
        }
    }
}
