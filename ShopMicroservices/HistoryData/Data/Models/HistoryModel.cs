using HistoryData.Attributes;
using HistoryData.Data.Models.Base;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HistoryData.Data.Models
{
    [NameCollection("History")]
    public class HistoryModel : IModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string User_Id { get; set; }
        public List<string> Orders_Id { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
