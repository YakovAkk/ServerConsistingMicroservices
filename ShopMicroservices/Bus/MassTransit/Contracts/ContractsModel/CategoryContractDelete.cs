using Bus.MassTransit.Contracts.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus.MassTransit.Contracts.ContractsModel
{
    public class CategoryContractDelete 
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? MessageWhatWrong { get; set; }
        public CategoryContractDelete()
        {

        }

        public CategoryContractDelete(string id)
        {
            Id = id;
        }
    }
}
