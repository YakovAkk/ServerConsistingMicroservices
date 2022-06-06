using LegoData.Data.Models;

namespace LegoBus.MassTransit.Contracts
{
    public class LegoContractCreate
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public CategoryModel Category { get; set; }
        public string? MessageWhatWrong { get; set; }

        public LegoContractCreate()
        {

        }

        public LegoContractCreate(string? id, 
            string name, string imageUrl, string
            description, uint price, bool isFavorite, 
            CategoryModel category, string? messageWhatWrong)
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
