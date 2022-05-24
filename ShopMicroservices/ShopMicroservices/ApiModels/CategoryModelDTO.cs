using ShopMicroservices.Models.Base;

namespace ShopMicroservices.Models
{
    public class CategoryModelDTO : IModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public CategoryModelDTO()
        {

        }

        public CategoryModelDTO(string? id, string name, string imageUrl, string? messageWhatWrong)
        {
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
