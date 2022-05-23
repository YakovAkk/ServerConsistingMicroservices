using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryData.Data.Models.Base
{
    public interface IModel
    {
        public string? Id { get; set; }
        public string Name { get; set; }
    }
}
