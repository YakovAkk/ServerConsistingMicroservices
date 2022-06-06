namespace AccountBus.MassTransit.Contracts
{
    public class AccountContractLogin
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        public DateTime DataRegistration { get; set; }
        public string? MessageThatWrong { get; set; }
    }
}
