using LegoData.Attibutes;
using LegoData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LegoData.Data.Models
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
        public string Category_Id { get; set; }
        public string? MessageWhatWrong { get; set; }
        public LegoModel()
        {

        }
    }
}
