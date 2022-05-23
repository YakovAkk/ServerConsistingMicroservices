using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services.Base;


namespace CategoryServices.Services
{
    public class CategoryService : BaseService<CategoryModel>
    {
        public CategoryService(RepositoryBase<CategoryModel> repository) : base(repository)
        {

        }
    }
}
