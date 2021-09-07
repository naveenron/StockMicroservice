using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace StockMicroservice.Models
{
    public class Stocks
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string StockId { get; set; }
        public string CompanyID { get; set; }
        public decimal StockPrice { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
