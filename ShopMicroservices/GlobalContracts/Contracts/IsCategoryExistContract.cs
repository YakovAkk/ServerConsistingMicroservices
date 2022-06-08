namespace GlobalContracts.Contracts
{
    public class IsCategoryExistContract
    {
        public bool IsCategoryExist { get; set; }
        public string CategoryId { get; set; }

        public IsCategoryExistContract()
        {
            CategoryId = "";
        }
    }
}
