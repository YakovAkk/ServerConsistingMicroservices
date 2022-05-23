using ShopMicroservices.Models.Base;

namespace ShopMicroservices.Models
{
    public class CategoryModel : IModel
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }

        public CategoryModel()
        {

        }

        public CategoryModel(string? id, string name, string imageUrl, string? messageWhatWrong)
        {
            Name = name;
            ImageUrl = imageUrl;
        }
    }
}
