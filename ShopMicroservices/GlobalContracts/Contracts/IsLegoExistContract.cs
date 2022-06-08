namespace GlobalContracts.Contracts
{
    public class IsLegoExistContract
    {
        public bool IsLegoExist { get; set; }
        public string LegoId { get; set; }

        public IsLegoExistContract()
        {
            LegoId = "";
        }
    }
}
