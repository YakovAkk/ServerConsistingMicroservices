using LegoBus.MassTransit.Contracts;
using LegoData.Data.Models;
using LegoRepository.RepositoriesMongo.Base;
using LegoService.DTOs;
using LegoService.Services.Base;
using MassTransit;

namespace LegoService.Services
{
    public class LegoService : BaseService<LegoModel, LegoModelDTO>, ILegoService
    {
        private readonly IRequestClient<LegoContractCreate> _clientCreate;
        private readonly IRequestClient<LegoContractUpdate> _clientUpdate;
        private readonly IRequestClient<LegoContractDelete> _clientDelete;
        public LegoService(ILegoRepository repository,
            IRequestClient<LegoContractCreate> clientCreate,
            IRequestClient<LegoContractUpdate> clientUpdate,
            IRequestClient<LegoContractDelete> clientDelete
            ) : base(repository)
        {
            _clientCreate = clientCreate;
            _clientUpdate = clientUpdate;
            _clientDelete = clientDelete;
        }

        public override async Task<LegoModel> AddAsync(LegoModelDTO item)
        {
            var response = await _clientCreate.GetResponse<LegoContractCreate>(item);

            if (response.Message == null)
            {
                return new LegoModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new LegoModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                Description = response.Message.Description,
                Price = response.Message.Price,
                isFavorite = response.Message.isFavorite,
                Category = response.Message.Category,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
        public override async Task<LegoModel> DeleteAsync(string id)
        {
            var legoId = new LegoContractDelete()
            {
                Id = id
            };

            var response = await _clientDelete.GetResponse<LegoContractDelete>(legoId);

            if (response.Message == null)
            {
                return new LegoModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new LegoModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                Description = response.Message.Description,
                Price = response.Message.Price,
                isFavorite = response.Message.isFavorite,
                Category = response.Message.Category,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
        public override async Task<LegoModel> UpdateAsync(LegoModelDTO item)
        {
            var response = await _clientUpdate.GetResponse<LegoContractUpdate>(item);

            if (response.Message == null)
            {
                return new LegoModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new LegoModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                Description = response.Message.Description,
                Price = response.Message.Price,
                isFavorite = response.Message.isFavorite,
                Category = response.Message.Category,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
    }
}
