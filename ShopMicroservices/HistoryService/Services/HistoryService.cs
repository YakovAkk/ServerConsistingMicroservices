using HistoryBus.MassTransit.Contracts;
using HistoryData.Data.Models;
using HistoryRepository.RepositoriesMongo.Base;
using HistoryService.DTOs;
using HistoryService.Services.Base;
using MassTransit;

namespace HistoryService.Services
{
    public class HistoryService : BaseService<HistoryModel, HistoryModelDTO>, IHistoryService
    {
        private readonly IRequestClient<HistoryContractCreate> _clientCreate;
        private readonly IRequestClient<HistoryContractUpdate> _clientUpdate;
        private readonly IRequestClient<HistoryContractDelete> _clientDelete;
        public HistoryService(IHistoryRepository repository,
            IRequestClient<HistoryContractCreate> clientCreate,
            IRequestClient<HistoryContractUpdate> clientUpdate,
            IRequestClient<HistoryContractDelete> clientDelete) : base(repository)
        {
            _clientCreate = clientCreate;
            _clientUpdate = clientUpdate;
            _clientDelete = clientDelete;
        }

        public override async Task<HistoryModel> AddAsync(HistoryModelDTO item)
        {
            var response = await _clientCreate.GetResponse<HistoryContractCreate>(item);

            if (response.Message == null)
            {
                return new HistoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new HistoryModel()
            {
                Id = response.Message.Id,
                User_Id = response.Message.User_Id,
                Orders_Id = response.Message.Orders_Id,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }

        public override async Task<HistoryModel> DeleteAsync(string id)
        {
            var historyId = new HistoryContractDelete()
            {
                Id = id
            };

            var response = await _clientDelete.GetResponse<HistoryContractDelete>(historyId);

            if (response.Message == null)
            {
                return new HistoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new HistoryModel()
            {
                Id = response.Message.Id,
                User_Id = response.Message.User_Id,
                Orders_Id = response.Message.Orders_Id,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }

        public override async Task<HistoryModel> UpdateAsync(HistoryModelDTO item)
        {
            var response = await _clientUpdate.GetResponse<HistoryContractUpdate>(item);

            if (response.Message == null)
            {
                return new HistoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new HistoryModel()
            {
                Id = response.Message.Id,
                User_Id = response.Message.User_Id,
                Orders_Id = response.Message.Orders_Id,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
    }
}
