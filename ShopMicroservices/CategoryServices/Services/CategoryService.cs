using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services.Base;


namespace CategoryServices.Services
{
    public class CategoryService : BaseService<CategoryModel> , ICategoryService 
    {
        public CategoryService(ICategoryRepository repository) : base(repository)
        {

        }
    }
}
