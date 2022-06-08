namespace GlobalContracts.Contracts
{
    public class IsUserExistContract
    {
        public bool IsUserExist { get; set; }
        public string UserId { get; set; }

        public IsUserExistContract()
        {
            UserId = "";
        }
    }
}
