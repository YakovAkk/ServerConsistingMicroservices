using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
using BasketData.Data.Models;
using BasketRepository.RepositoriesMongo.Base;
using BasketService.DTOs;
using BasketService.Services.Base;
using MassTransit;

namespace BasketService.Services
{
    public class BasketService : BaseService<BasketModel, BasketModelDTO>, IBasketService
    {
        private readonly IRequestClient<BasketContractCreate> _clientCreate;
        private readonly IRequestClient<BasketContractUpdate> _clientUpdate;
        private readonly IRequestClient<BasketContractDelete> _clientDelete;
        public BasketService(IBasketRepository repository, 
            IRequestClient<BasketContractCreate> clientCreate, 
            IRequestClient<BasketContractUpdate> clientUpdate,
            IRequestClient<BasketContractDelete> clientDelete) : base(repository)
        {
            _clientCreate = clientCreate;
            _clientUpdate = clientUpdate;
            _clientDelete = clientDelete;

        }

        public override async Task<BasketModel> AddAsync(BasketModelDTO item)
        {
            var model = new BasketContractCreate()
            {
                Lego_Id = item.Lego_Id,
                User_Id = item.User_Id,
                Amount = item.Amount,
                DateDeal = item.DateDeal
            };

            var request = await _clientCreate.GetResponse<BasketContractCreate>(model);

            if (request.Message == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new BasketModel()
            {
                Id = request.Message.Id,
                Lego_Id = request.Message.Lego_Id,
                User_Id = request.Message.User_Id,
                Amount = request.Message.Amount,
                DateDeal = request.Message.DateDeal,
                MessageWhatWrong = request.Message.MessageWhatWrong
            };
        }

        public  override async Task<BasketModel> DeleteAsync(string id)
        {
            var request = new BasketContractDelete()
            {
                Id = id
            };

            var response = await _clientDelete.GetResponse<BasketContractDelete>(request);

            if (response.Message == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new BasketModel()
            {
                Id = response.Message.Id,
                Lego_Id = response.Message.Lego_Id,
                User_Id = response.Message.User_Id,
                Amount = response.Message.Amount,
                DateDeal = response.Message.DateDeal,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }

        public override async Task<BasketModel> UpdateAsync(BasketModelDTO item)
        {
            var model = new BasketContractUpdate()
            {
                Lego_Id = item.Lego_Id,
                User_Id = item.User_Id,
                Amount = item.Amount,
                DateDeal = item.DateDeal
            };
            var request = await _clientUpdate.GetResponse<BasketContractUpdate>(model);

            if (request.Message == null)
            {
                return new BasketModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new BasketModel()
            {
                Id = request.Message.Id,
                Lego_Id = request.Message.Lego_Id,
                User_Id = request.Message.User_Id,
                Amount = request.Message.Amount,
                DateDeal = request.Message.DateDeal,
                MessageWhatWrong = request.Message.MessageWhatWrong
            };
        }
    }
}
