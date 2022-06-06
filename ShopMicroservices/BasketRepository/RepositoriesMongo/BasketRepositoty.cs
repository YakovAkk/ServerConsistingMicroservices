using BasketData.Data.Base.Models;
using BasketData.Data.DatabaseMongo;
using BasketData.Data.Models;
using BasketRepository.RepositoriesMongo.Base;
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

            var addLego = Lego.FirstOrDefault(i => (i.Lego.Name == item.Lego.Name) && (i.User.Name == item.User.Name));

            if (addLego != null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Database has already the lego";
                return basket;
            }

            var document = new BasketModel()
            {
                Lego = item.Lego,
                User = item.User,
                Amount = item.Amount,
                DateDeal = item.DateDeal
            };

            await Collection.InsertOneAsync(document);

            var result = await FindByLegoAndUserAsync(document.Lego, document.User);

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
            var Lego = await GetAllAsync();
            if (Lego == null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Database has't any lego";
                return basket;
            }

            var addLego = Lego.FirstOrDefault(i => (i.Lego.Name == item.Lego.Name) && (i.User.Name == item.User.Name));

            if (addLego != null)
            {
                var basket = new BasketModel();
                basket.MessageWhatWrong = "Database has already the lego";
                return basket;

            }

            var document = new BasketModel()
            {
                Lego = item.Lego,
                User = item.User,
                Amount = item.Amount,
                DateDeal = item.DateDeal
            };

            var resultUpdate = await Collection.UpdateOneAsync(i => i.Id == item.Id, Builders<BasketModel>.Update
               .Set(b => b.Lego, document.Lego)
               .Set(b => b.User, document.User)
               .Set(b => b.Amount, document.Amount));

            var result = await FindByLegoAndUserAsync(document.Lego, document.User);

            if (result == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "The BasketItem wasn't append"
                };
            }

            return result;
        }
        private async Task<BasketModel> FindByLegoAndUserAsync(LegoModel legoModel, UserModel userModel)
        {
            var result = await GetAllAsync();

            var data = result.FirstOrDefault(b => b.User.Id == userModel.Id && b.Lego.Id == legoModel.Id);


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
    }
}
