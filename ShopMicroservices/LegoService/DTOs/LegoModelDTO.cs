namespace LegoService.DTOs
{
    public class LegoModelDTO
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public string Category_Id { get; set; }
    }
}
