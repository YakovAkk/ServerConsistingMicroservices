using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.DTOs
{
    public class BasketModelDTO
    {
        public LegoModelDTO Lego { get; set; }
        public UserModelDTO User { get; set; }
        public uint Amount { get; set; }
        public DateTime DateDeal { get; set; }
        public string? messageThatWrong { get; set; }
    }
}
