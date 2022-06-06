using BasketBus.MassTransit.Contracts;
using BasketData.Data.Base.Models;
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
        public BasketService(IBasketRepository repository) : base(repository)
        {
        }

        public override async Task<BasketModel> AddAsync(BasketModelDTO item)
        {
            var request = await _clientCreate.GetResponse<BasketContractCreate>(item);

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
                Lego = request.Message.Lego,
                User = request.Message.User,
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
                Lego = response.Message.Lego,
                User = response.Message.User,
                Amount = response.Message.Amount,
                DateDeal = response.Message.DateDeal,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }

        public override async Task<BasketModel> UpdateAsync(BasketModelDTO item)
        {
            var request = await _clientUpdate.GetResponse<BasketContractUpdate>(item);

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
                Lego = request.Message.Lego,
                User = request.Message.User,
                Amount = request.Message.Amount,
                DateDeal = request.Message.DateDeal,
                MessageWhatWrong = request.Message.MessageWhatWrong
            };
        }
    }
}
