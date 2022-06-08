using HistoryData.Data.DatabaseNoSql;
using HistoryData.Data.Models;
using HistoryRepository.RepositoriesMongo.Base;
using MongoDB.Driver;

namespace HistoryRepository.RepositoriesMongo
{
    public class HistoryRepository : RepositoryBase<HistoryModel>, IHistoryRepository
    {
        public HistoryRepository(MongoDatabase<HistoryModel> mongoDatabase) : base(mongoDatabase)
        {
        }

        protected override IMongoCollection<HistoryModel> Collection { get ; set ; }

        public override async Task<HistoryModel> AddAsync(HistoryModel item)
        {
            if (item == null)
            {
                var history = new HistoryModel();

                history.MessageWhatWrong = "Item was null";

                return history;
            }

            var allItems = await GetAllAsync();

            if (allItems.Count == 0)
            {
                await Collection.InsertOneAsync(item);

                allItems = await GetAllAsync();

                var resp = allItems.FirstOrDefault(i => i.User_Id == item.User_Id);

                return resp;
            }

            var res = allItems.FirstOrDefault(i => i.User_Id == item.User_Id);

            if(res == null)
            {
                var responce = new HistoryModel()
                {
                    MessageWhatWrong = "User doesn't exist"
                };

                return responce;
            }

            res.Orders_Id.AddRange(item.Orders_Id);

            var result = await Collection.UpdateOneAsync(i => i.Id == res.Id, Builders<HistoryModel>.Update
                .Set(o => o.Orders_Id, res.Orders_Id));

            return res;
        }
        public override async Task<HistoryModel> DeleteAsync(string id)
        {
            var data = await GetByIDAsync(id);

            if (data.MessageWhatWrong != null)
            {
                var responce = new HistoryModel()
                {
                    MessageWhatWrong = "item isn't exists"
                };

                return responce;
            }

            await Collection.DeleteOneAsync(i => i.Id == id);

            return data;
        }
        public override async Task<HistoryModel> GetByIDAsync(string id)
        {
            var allItems = await GetAllAsync();
            if (allItems == null)
            {
                return new HistoryModel()
                {
                    MessageWhatWrong = "The database doesn't cotain any category"
                };
            }

            var data = allItems.FirstOrDefault(i => i.Id == id);

            if (data == null)
            {
                return new HistoryModel()
                {
                    MessageWhatWrong = "The History doesn't exist"
                };
            }

            return data;
        }
        public override async Task<HistoryModel> UpdateAsync(HistoryModel item)
        {
            if (item == null)
            {
                var history = new HistoryModel();

                history.MessageWhatWrong = "Item was null";

                return history;
            }

            var allItems = await GetAllAsync();

            if (allItems != null)
            {
                var responce = new HistoryModel()
                {
                    MessageWhatWrong = "History Wasn't updated"
                };
            }

            var res = allItems.FirstOrDefault(i => i.User_Id == item.User_Id);

            if (res == null)
            {
                var responce = new HistoryModel()
                {
                    MessageWhatWrong = "User doesn't exist"
                };

                return responce;
            }

            res.Orders_Id.AddRange(item.Orders_Id);

            var result = await Collection.UpdateOneAsync(i => i.Id == res.Id, Builders<HistoryModel>.Update
                .Set(o => o.Orders_Id, res.Orders_Id));

            return res;
        }
    }
}
