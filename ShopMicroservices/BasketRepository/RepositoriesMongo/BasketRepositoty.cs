using BasketData.Data.Base.Models;
using BasketData.Data.DatabaseMongo;
using BasketRepository.RepositoriesMongo.Base;
using GlobalContracts.Contracts;
using MassTransit;
using MongoDB.Driver;

namespace BasketRepository.RepositoriesMongo
{
    public class BasketRepositoty : RepositoryBase<BasketModel>, IBasketRepository
    {

        protected override IMongoCollection<BasketModel> Collection { get; set; }
        public BasketRepositoty(MongoDatabase<BasketModel> mongoDatabase) : base(mongoDatabase)
        {

        }
        public override async Task<BasketModel> AddAsync(BasketModel item)
        {
            var Lego = await GetAllAsync();

            if (Lego == null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Database has't any lego";
                return basket;
            }

            var addLego = Lego.FirstOrDefault(i => (i.Lego_Id == item.Lego_Id) && (i.User_Id == item.User_Id));

            if (addLego != null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Database has already the lego";
                return basket;
            }


            var document = new BasketModel()
            {
                Lego_Id = item.Lego_Id,
                User_Id = item.User_Id,
                Amount = item.Amount,
                DateDeal = item.DateDeal
            };

            await Collection.InsertOneAsync(document);

            var result = await FindByLegoAndUserAsync(document.Lego_Id, document.User_Id);

            if (result == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The BasketItem wasn't append"
                };
            }

            return result;



        }
        public override async Task<BasketModel> UpdateAsync(BasketModel item)
        {
            if (item == null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Item was null";
                return basket;
            }




            var result = await GetByIDAsync(item.Id);

            if (result == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The BasketItem doesn't contain in the database"
                };
            }

            var resultUpdate = await Collection.UpdateOneAsync(i => i.Id == item.Id, Builders<BasketModel>.Update
               .Set(b => b.Lego_Id, item.Lego_Id)
               .Set(b => b.User_Id, item.User_Id)
               .Set(b => b.Amount, item.Amount)
               .Set(b => b.DateDeal, item.DateDeal)
               .Set(b => b.MessageWhatWrong, item.MessageWhatWrong));

            if (resultUpdate == null)
            {
                var basket = new BasketModel()
                {
                    MessageWhatWrong = "The element hasn't contained in database"
                };
                return basket;
            }

            result = await FindByLegoAndUserAsync(item.Lego_Id, item.User_Id);

            if (result == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The BasketItem wasn't Updated"
                };
            }

            return result;




        }
        private async Task<BasketModel> FindByLegoAndUserAsync(string legoId, string userId)
        {
            var result = await GetAllAsync();

            var data = result.FirstOrDefault(b => (b.User_Id == userId) && (b.Lego_Id == legoId));

            return data;
        }
        public override async Task<BasketModel> DeleteAsync(string id)
        {
            var result = await GetByIDAsync(id);

            if (result == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "Database doesn't contait the Basket"
                };
            }

            await Collection.DeleteOneAsync(i => i.Id == id);

            return result;
        }
        public override async Task<BasketModel> GetByIDAsync(string id)
        {
            var allItems = await GetAllAsync();
            if (allItems == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The database doesn't cotain any category"
                };
            }

            var data = allItems.FirstOrDefault(i => i.Id == id);

            if (data == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The basket doesn't exist"
                };
            }

            return data;
        }
    }
}
