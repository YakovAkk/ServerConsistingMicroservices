using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrocerviceContract.Contracts.Interfaces
{
    public interface IContract
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? messageWhatWrong { get; set; }

    }
}
