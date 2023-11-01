using MongoDB.Driver;
using OtoGaleri.Models;

namespace OtoGaleri.Interface
{
    public interface IOtoGaleri
    {
        IMongoCollection<Arabalar> arabalarcollection { get; }
        //sonradan eklenen
        IMongoCollection<Account> kullanicicollection { get; }
        IEnumerable<Account> GetAllKullanicilar();
        IEnumerable<Arabalar>GetAllArabalar();
        //eklenen
       
        Arabalar GetArabalarDetails(string Model);
        void Create(Arabalar arabalarData);
        void Update(string _id,Arabalar arabalarData);
        void Delete(string Model);
    }
}
