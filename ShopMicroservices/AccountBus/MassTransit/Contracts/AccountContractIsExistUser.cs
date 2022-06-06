

namespace AccountBus.MassTransit.Contracts
{
    public class AccountContractIsExistUser
    {
        public string Email { get; set; }
        public bool IsExistUser { get; set; }
        public string MessageThatWrong { get; set; }
    }
}
