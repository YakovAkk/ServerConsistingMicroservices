using GlobalContracts.Contracts;
using LegoData.Data.DatabaseMongo;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using MassTransit;
using MongoDB.Driver;

namespace LegoRepository.RepositoriesMongo
{
    public class LegoRepository : BaseRepository<LegoModel>, ILegoRepository
    {

        protected override IMongoCollection<LegoModel> Collection { get; set; }
        public LegoRepository(MongoDatabase<LegoModel> mongoDatabase) : base(mongoDatabase)
        {

        }
        public override async Task<LegoModel> AddAsync(LegoModel item)
        {
            if (item == null)
            {
                var lego = new LegoModel();

                lego.MessageWhatWrong = "Item was null";

                return lego;
            }


            var document = new LegoModel()
            {
                Name = item.Name,
                ImageUrl = item.ImageUrl,
                Description = item.Description,
                Price = item.Price,
                isFavorite = item.isFavorite,
                Category_Id = item.Category_Id
            };

            await Collection.InsertOneAsync(document);

            var result = await GetByNameAsync(item.Name);

            if (result == null)
            {
                var lego = new LegoModel();

                lego.MessageWhatWrong = "Can't add item to database";

                return lego;
            }

            return result;
        }
        private async Task<LegoModel> GetByNameAsync(string name)
        {
            var allItems = await GetAllAsync();

            return allItems.FirstOrDefault(i => i.Name == name);
        }
        public override async Task<LegoModel> UpdateAsync(LegoModel item)
        {
            if (item == null)
            {
                var lego = new LegoModel();
                lego.MessageWhatWrong = "Item was null";
                return lego;
            }


            var result = await Collection.UpdateOneAsync(i => i.Id == item.Id, Builders<LegoModel>.Update
           .Set(l => l.Name, item.Name)
           .Set(l => l.ImageUrl, item.ImageUrl)
           .Set(l => l.Description, item.Description)
           .Set(l => l.Price, item.Price)
           .Set(l => l.isFavorite, item.isFavorite)
           .Set(l => l.Category_Id, item.Category_Id));

            if (result == null)
            {
                var lego = new LegoModel();
                lego.MessageWhatWrong = "The element hasn't contained in database";
                return lego;
            }

            var resultItem = await GetByNameAsync(item.Name);

            if (resultItem == null)
            {
                var lego = new LegoModel();
                lego.MessageWhatWrong = "Can't update item to database";
                return lego;
            }
            return resultItem;

        }
        public override async Task<LegoModel> DeleteAsync(string id)
        {
            var data = await GetByIDAsync(id);

            if (data.MessageWhatWrong != null)
            {
                var responce = new LegoModel()
                {
                    MessageWhatWrong = "item isn't exists"
                };

                return responce;
            }

            await Collection.DeleteOneAsync(i => i.Id == id);

            return data;
        }
        public override async Task<LegoModel> GetByIDAsync(string id)
        {
            var allItems = await GetAllAsync();
            if (allItems == null)
            {
                return new LegoModel()
                {
                    MessageWhatWrong = "The database doesn't cotain any category"
                };
            }

            var data = allItems.FirstOrDefault(i => i.Id == id);

            if (data == null)
            {
                return new LegoModel()
                {
                    MessageWhatWrong = "The category doesn't exist"
                };
            }

            return data;
        }
    }
}
