namespace LegoService.DTOs
{
    public class LegoModelDTO
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public CategoryModelDTO Category { get; set; }
        public string? MessageWhatWrong { get; set; }
        public LegoModelDTO()
        {

        }
    }
}
