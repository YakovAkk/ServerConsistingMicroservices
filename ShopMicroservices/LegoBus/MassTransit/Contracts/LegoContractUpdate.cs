﻿using LegoData.Data.Models;

namespace LegoBus.MassTransit.Contracts
{
    public class LegoContractUpdate
    {  
        public string? Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public uint Price { get; set; }
        public bool isFavorite { get; set; }
        public CategoryModel Category { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}