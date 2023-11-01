using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtoGaleri.Interface;
using OtoGaleri.Models;

namespace OtoGaleri.Controllers
{
    public class ArabalarController : Controller
    {
        private readonly IOtoGaleri _context;

        public ArabalarController(IOtoGaleri context)
        {
            _context = context;
        }

        [Authorize]
        public IActionResult Index()
        {
            
            return View(_context.GetAllArabalar());
        }

        //public IActionResult Index(string marka=null,decimal? fiyat=null)
        // {

        // }
        // filter
        [HttpGet]
        [Route("arabalar/filter/{min}/{max}")]
        public IActionResult filter(decimal min,decimal max)
        {
            var arabafilter = _context.GetAllArabalar().Where(p => p.Fiyat >= min && p.Fiyat <= max);
            return new JsonResult(arabafilter);
        }
        //Arama çubuğu
        [HttpGet]
        public async Task<IActionResult> Index(string Arasearch)
        {
            ViewData["GetArabalarDetails"] = Arasearch;
            var aracquery = from x in _context.GetAllArabalar() select x;
            if (!string.IsNullOrEmpty(Arasearch))
            {
                aracquery = aracquery.Where(a => a.Model.Contains(Arasearch));
            }
            return View(aracquery.ToList());
            
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreatePost(Arabalar arabadata)
        {
            _context.Create(arabadata);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Edit(string Model)
        {
            var md = _context.GetArabalarDetails(Model);
            return View(md);
        }
        [HttpPost]
        public IActionResult EditPost(string _id,Arabalar arabadata)
        {
            _context.Update(_id, arabadata);
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Details(string Model)
        {
            var md = _context.GetArabalarDetails(Model);
            return View(md);
        }
        [HttpGet]
        public IActionResult Delete(string Model)
        {
            var md = _context.GetArabalarDetails(Model);
            return View(md);
        }
        [HttpPost]
        public IActionResult DeletePost(string Model)
        {
            _context.Delete(Model);
            return RedirectToAction("Index");
        }
    }
}
