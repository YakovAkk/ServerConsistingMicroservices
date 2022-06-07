using ShopMicroservices.Models;
using ShopMicroservices.Models.Base;

namespace ShopMicroservices.ApiModels
{
    public class LegoModelDTO : IModelDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public CategoryModelDTO Category { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}
