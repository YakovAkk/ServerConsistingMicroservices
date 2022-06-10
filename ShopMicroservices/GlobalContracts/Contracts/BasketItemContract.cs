﻿namespace GlobalContracts.Contracts
{
    public class BasketItemContract
    {
        public string? Id { get; set; }
        public string Lego_Id { get; set; }
        public string User_Id { get; set; }
        public int Amount { get; set; }
        public DateTime DateDeal { get; set; }
        public string? MessageWhatWrong { get; set; }
    }
}