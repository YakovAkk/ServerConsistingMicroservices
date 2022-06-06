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
        public CategoryModel Category { get; set; }
        public string? MessageWhatWrong { get; set; }
        public LegoModel()
        {

        }

        public LegoModel(string? id, string name, string imageUrl, string description, uint price, bool isFavorite, CategoryModel category, string? messageWhatWrong)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            Description = description;
            Price = price;
            this.isFavorite = isFavorite;
            Category = category;
            MessageWhatWrong = messageWhatWrong;
        }
    }
}
