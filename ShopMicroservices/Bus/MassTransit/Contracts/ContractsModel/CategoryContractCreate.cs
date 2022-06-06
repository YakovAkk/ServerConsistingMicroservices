namespace Bus.MassTransit.Contracts.ContractsModel
{
    public class CategoryContractCreate
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string? MessageWhatWrong { get; set; }
        public CategoryContractCreate()
        {

        }

        public CategoryContractCreate(string? id ,string name, string imageUrl, string? messageWhatWrong)
        {
            Id = id;
            Name = name;
            ImageUrl = imageUrl;
            MessageWhatWrong = messageWhatWrong;
        }
    }
}
