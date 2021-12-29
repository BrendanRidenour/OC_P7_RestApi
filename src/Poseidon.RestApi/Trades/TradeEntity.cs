﻿namespace Poseidon.RestApi.Trades
{
    public class TradeEntity : Internal.EntityBase
    {
        //public int TradeId { get; set; }
        public string Account { get; set; }
        public string Type { get; set; }
        public double BuyQuantity { get; set; }
        public double SellQuantity { get; set; }
        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public string Benchmark { get; set; }
        public DateTimeOffset TradeDate { get; set; }
        public string Security { get; set; }
        public string Status { get; set; }
        public string Trader { get; set; }
        public string Book { get; set; }
        public string CreationName { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string RevisionName { get; set; }
        public DateTimeOffset RevisionDate { get; set; }
        public string DealName { get; set; }
        public string DealType { get; set; }
        public string SourceListId { get; set; }
        public string Side { get; set; }
    }
}