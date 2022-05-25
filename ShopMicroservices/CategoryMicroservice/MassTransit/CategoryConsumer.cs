using CategoryData.Data.Models;
using CategoryServices.Services.Base;
using MassTransit;
using MicrocerviceContract.Contracts.CategoryContracts;

namespace CategoryMicroservice.MassTransit
{
    public class CategoryConsumer : IConsumer<CategoryContractCreate>
    {
        private readonly ICategoryService _categoryService;
        private readonly IPublishEndpoint _publishEndpoint;
        public CategoryConsumer(ICategoryService categoryService, IPublishEndpoint publishEndpoint)
        {
            _categoryService = categoryService;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<CategoryContractCreate> context)
        {
            var category = new CategoryModel()
            {
                Name = context.Message.Name,
                ImageUrl = context.Message.ImageUrl
            };

            var result = await _categoryService.AddAsync(category);

            if(result != null)
            {
                await _publishEndpoint.Publish<CategoryContractCreate>(result);
            }
        }
    }
}
