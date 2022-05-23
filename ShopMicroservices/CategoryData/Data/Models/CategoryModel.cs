using CategoryData.Attributes;
using CategoryData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace CategoryData.Data.Models
{
    [NameCollection("Categories")]
    public class CategoryModel : IModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? messageWhatWrong { get; set; }

        public CategoryModel()
        {

        }

        public CategoryModel(string? id, string name, string imageUrl, string? messageWhatWrong)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            this.messageWhatWrong = messageWhatWrong;
        }
    }
}
