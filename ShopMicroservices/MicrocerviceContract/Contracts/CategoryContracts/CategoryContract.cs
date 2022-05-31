using MicrocerviceContract.Contracts.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrocerviceContract.Contracts.CategoryContracts
{
    public class CategoryContract : IContract
    {
        public string? Id { get; set ; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? messageWhatWrong { get ; set; }

        public CategoryContract()
        {

        }

        public CategoryContract(string name, string imageUrl)
        {
            Name = name;
            ImageUrl = imageUrl;
        }

        public CategoryContract(string? id, string name, string imageUrl, string? messageWhatWrong)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            this.messageWhatWrong = messageWhatWrong;
        }
    }
}
