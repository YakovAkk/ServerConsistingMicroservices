using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountBus.MassTransit.Contracts
{
    public class AccountContractIsExistUser
    {
        public string Email { get; set; }
        public bool IsExistUser { get; set; }
        public string MessageThatWrong { get; set; }
    }
}
