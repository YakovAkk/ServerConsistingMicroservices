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
        private readonly IRequestClient<IsLegoExistContract> _isLegoExistClient;
        private readonly IRequestClient<IsUserExistContract> _isUserExistClient;
        protected override IMongoCollection<BasketModel> Collection { get; set; }
        public BasketRepositoty(MongoDatabase<BasketModel> mongoDatabase,
            IRequestClient<IsLegoExistContract> isLegoExistClient,
            IRequestClient<IsUserExistContract> isUserExistClient) : base(mongoDatabase)
        {
            _isLegoExistClient = isLegoExistClient;
            _isUserExistClient = isUserExistClient;
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
           
            var legoIdModel = new IsLegoExistContract()
            {
                LegoId = item.Lego_Id
            };

            var userIdModel = new IsUserExistContract()
            {
                UserId = item.User_Id
            };

            var isLegoExist = await _isLegoExistClient.GetResponse<IsLegoExistContract>(legoIdModel);
            var isUserExist = await _isUserExistClient.GetResponse<IsUserExistContract>(userIdModel);

            if(isLegoExist.Message.IsLegoExist && isUserExist.Message.IsUserExist)
            {
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

            var resBasket = new BasketModel();
            resBasket.MessageWhatWrong = "Lego Or User don't exist";
            return resBasket;
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

            var legoIdModel = new IsLegoExistContract()
            {
                LegoId = item.Lego_Id
            };

            var useIdModel = new IsUserExistContract()
            {
                UserId = item.User_Id
            };

            var isLegoExist = await _isLegoExistClient.GetResponse<IsLegoExistContract>(legoIdModel);
            var isUserExist = await _isUserExistClient.GetResponse<IsUserExistContract>(useIdModel);

            if (isLegoExist.Message.IsLegoExist && isUserExist.Message.IsUserExist)
            {
                var resultUpdate = await Collection.UpdateOneAsync(i => i.Id == item.Id, Builders<BasketModel>.Update
                   .Set(b => b.Lego_Id, item.Lego_Id)
                   .Set(b => b.User_Id, item.User_Id)
                   .Set(b => b.Amount, item.Amount)
                   .Set(b => b.DateDeal, item.DateDeal));

                if (resultUpdate == null)
                {
                    var basket = new BasketModel()
                    {
                        MessageWhatWrong = "The element hasn't contained in database"
                    };
                    return basket;
                }

                var result = await FindByLegoAndUserAsync(item.Lego_Id, item.User_Id);

                if (result == null)
                {
                    return new BasketModel()
                    {
                        MessageWhatWrong = "The BasketItem wasn't Updated"
                    };
                }

                return result;
            }

            var resBasket = new BasketModel();
            resBasket.MessageWhatWrong = "Lego Or User don't exist";
            return resBasket;

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
    }
}
