
namespace Bus.MassTransit.Contracts.ContractsModel
{
    public class CategoryContractDelete 
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? ImageUrl { get; set; }
        public string? MessageWhatWrong { get; set; }
        public CategoryContractDelete()
        {

        }

        public CategoryContractDelete(string id)
        {
            Id = id;
        }
    }
}
