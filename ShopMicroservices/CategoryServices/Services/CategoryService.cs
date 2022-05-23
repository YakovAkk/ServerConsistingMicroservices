using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services.Base;


namespace CategoryServices.Services
{
    public class CategoryService : BaseServiceForMongo<CategoryModel>
    {
        public CategoryService(MongoDbBase<CategoryModel> repository) : base(repository)
        {

        }
    }
}
