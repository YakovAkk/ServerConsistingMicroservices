using BasketData.Attibutes;
using BasketData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasketData.Data.Models
{
    [NameCollection("Lego")]
    public class LegoModel : IModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public CategoryModel Category { get; set; }
        public string? MessageThatWrong { get; set; }

        public LegoModel()
        {

        }

    }
}
