
using Bus.MassTransit.Contracts.ContractsModel;
using CategoryData.Data.Models;
using CategoryRepositories.RepositoriesMongo.Base;
using CategoryServices.Services.Base;
using MassTransit;

namespace CategoryServices.Services
{
    public class CategoryService : BaseService<CategoryModel>, ICategoryService
    {
        private readonly IRequestClient<CategoryContractCreate> _clientCreate;
        private readonly IRequestClient<CategoryContractUpdate> _clientUpdate;
        private readonly IRequestClient<CategoryContractDelete> _clientDelete;

        public CategoryService(
            ICategoryRepository repository,
            IRequestClient<CategoryContractCreate> clientCreate,
            IRequestClient<CategoryContractUpdate> clientUpdate, 
            IRequestClient<CategoryContractDelete> clientDelete) : base(repository)
        {
            _clientCreate = clientCreate;
            _clientUpdate = clientUpdate;
            _clientDelete = clientDelete;
        }

        public override async Task<CategoryModel> AddAsync(CategoryModel item)
        {
            var response = await _clientCreate.GetResponse<CategoryContractCreate>(item);

            if (response == null)
            {
                return new CategoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new CategoryModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
        public override async Task<CategoryModel> DeleteAsync(string id)
        {
            var categoryId = new CategoryContractDelete() { Id = id };

            var response = await _clientDelete.GetResponse<CategoryContractDelete>(categoryId);

            if (response == null)
            {
                return new CategoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new CategoryModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
        public override async Task<CategoryModel> UpdateAsync(CategoryModel item)
        {
            var response = await _clientUpdate.GetResponse<CategoryContractUpdate>(item);

            if (response == null)
            {
                return new CategoryModel()
                {
                    MessageWhatWrong = "response is null"
                };
            }

            return new CategoryModel()
            {
                Id = response.Message.Id,
                Name = response.Message.Name,
                ImageUrl = response.Message.ImageUrl,
                MessageWhatWrong = response.Message.MessageWhatWrong
            };
        }
    }
}
