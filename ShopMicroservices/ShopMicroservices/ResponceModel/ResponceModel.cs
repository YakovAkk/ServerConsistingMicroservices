namespace ShopMicroservices.Model
{
    public class ResponceModel
    {
       
        public bool IsSuccess { get; set; }
        public string Data { get; set; }

        public ResponceModel(bool isSuccess, string data)
        {
            IsSuccess = isSuccess;
            Data = data;
        }
        public ResponceModel()
        {

        }

    }
}
