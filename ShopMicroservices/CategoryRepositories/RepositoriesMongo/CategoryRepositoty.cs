using CategoryData.Data.DatabaseNoSql;
using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using MongoDB.Driver;


namespace CategoryRepositories.RepositoriesMongo
{
    public class CategoryRepositoty : RepositoryBase<CategoryModel> , ICategoryRepository
    {
        public CategoryRepositoty(MongoDatabase<CategoryModel> mongoDatabase) : base(mongoDatabase)
        {
        }

        protected override IMongoCollection<CategoryModel> Collection { get; set; }
        public override async Task<CategoryModel> AddAsync(CategoryModel item)
        {
            if (item == null)
            {
                var category = new CategoryModel();

                category.MessageWhatWrong = "Item was null";

                return category;
            }

            var document = new CategoryModel() { Name = item.Name, ImageUrl = item.ImageUrl };

            await Collection.InsertOneAsync(document);

            var result = await GetByNameAsync(item.Name);

            if(result == null)
            {
                var category = new CategoryModel();

                category.MessageWhatWrong = "Can't add item to database";

                return category;
            }

            return result;
        }
        public override async Task<CategoryModel> UpdateAsync(CategoryModel item)
        {

            if (item == null)
            {
                var category = new CategoryModel();

                category.MessageWhatWrong = "Item was null";

                return category;
            }

            var result = await Collection.UpdateOneAsync(i => i.Id == item.Id, Builders<CategoryModel>.
               Update.Set(c => c.Name, item.Name).Set(c => c.ImageUrl, item.ImageUrl));

            if (result == null)
            {
                var category = new CategoryModel();
                category.MessageWhatWrong = " The element hasn't contained in database";
                return category;
            }

            var resultItem = await GetByNameAsync(item.Name);

            if (resultItem == null)
            {
                var category = new CategoryModel();

                category.MessageWhatWrong = "Can't update item to database";

                return category;
            }

            return resultItem;
        }

        
    }
}

