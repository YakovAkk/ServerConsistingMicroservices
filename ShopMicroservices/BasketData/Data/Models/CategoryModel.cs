using BasketData.Attibutes;
using BasketData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BasketData.Data.Models
{
    [NameCollection("Categories")]
    public class CategoryModel : IModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? MessageThatWrong { get; set; }

        public CategoryModel()
        {

        }
    }
}
