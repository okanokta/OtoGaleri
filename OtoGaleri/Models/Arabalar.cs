using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OtoGaleri.Models
{
    public class Arabalar
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string _id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int Yıl { get; set; }

        [DisplayFormat(DataFormatString = "{0:#,0}")]
        public decimal Fiyat { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }
    }
}
