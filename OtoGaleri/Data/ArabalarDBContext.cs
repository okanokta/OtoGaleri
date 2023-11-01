using Microsoft.Extensions.Options;
using MongoDB.Driver;
using OtoGaleri.Interface;
using OtoGaleri.Models;
using System.Security.Cryptography;

namespace OtoGaleri.Data
{
    public class ArabalarDBContext : IOtoGaleri
    {
        public readonly IMongoDatabase _db;
   
        public ArabalarDBContext(IOptions<Settings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.Database);
        }
        public IMongoCollection<Arabalar> arabalarcollection => 
            //değişti -> Arabalar : araba oldu
            _db.GetCollection<Arabalar>("araba");
        //Sonradan eklendi
        public IMongoCollection<Account> kullanicicollection =>
            //değişti -> Arabalar : araba oldu
            _db.GetCollection<Account>("kullanici");

        public IEnumerable<Arabalar> GetAllArabalar()
        {
            return arabalarcollection.Find(a => true).ToList();
        }
        //Arama Çubuğu
       
        public Arabalar GetArabalarDetails(string Model)
        {
            var arabalardetay = arabalarcollection.Find(m => m.Model == Model).FirstOrDefault();
            return arabalardetay;
        }

        public void Create(Arabalar arabalarData)
        {
            arabalarcollection.InsertOne(arabalarData);
        }
        public void Update(string _id, Arabalar arabalarData)
        {
            var filter = Builders<Arabalar>.Filter.Eq(c => c._id, _id);
            var update = Builders<Arabalar>.Update
                .Set("Marka", arabalarData.Marka)
                .Set("Model", arabalarData.Model)
                .Set("Yıl", arabalarData.Yıl)
                .Set("Fiyat", arabalarData.Fiyat)
                .Set("ImageUrl", arabalarData.ImageUrl);

            arabalarcollection.UpdateOne(filter, update);

        }

        public void Delete(string Model)
        {
            var filter = Builders<Arabalar>.Filter.Eq(c => c.Model, Model);
            arabalarcollection.DeleteOne(filter);
        }
        //sonradan eklenen
        public IEnumerable<Account> GetAllKullanicilar()
        {
            return kullanicicollection.Find(a => true).ToList();
        }
    }
}
