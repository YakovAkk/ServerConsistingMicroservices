using BasketData.Attibutes;
using BasketData.Data.Models;
using BasketData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasketData.Data.Base.Models
{
    [NameCollection("Basket")]
    public class BasketModel : IModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Lego_Id { get; set; }
        public string User_Id { get; set; }
        public int Amount { get; set; }
        public DateTime DateDeal { get; set; }
        public string? MessageWhatWrong { get; set; }

        public BasketModel()
        {
            DateDeal = DateTime.Now;
        }
    }
}
