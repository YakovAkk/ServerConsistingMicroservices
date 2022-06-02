
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBus.MassTransit.Contracts
{
    public class AccountContractDelete
    {
        public string Id { get; set; }
        public string NickName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool RememberMe { get; set; }
        public DateTime DataRegistration { get; set; }
        public string? MessageThatWrong { get; set; }
    }
}
