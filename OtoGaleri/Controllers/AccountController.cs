using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using OtoGaleri.Data;
using OtoGaleri.Interface;
using OtoGaleri.Models;
using System.Security.Claims;

namespace OtoGaleri.Controllers
{
    public class AccountController : Controller
    {
        private readonly IOtoGaleri _context;
        public AccountController(IOtoGaleri context)
        {
            _context = context;
        }
        // GET: AccountController
        [HttpGet]
        public ActionResult GirisYap()
        {
            return View();
        }
        public async Task<ActionResult> GirisYap(Account a)
        {
            var bilgiler = _context.kullanicicollection.Find(x => x.KulAdi == a.KulAdi &&
                x.Sifre == a.Sifre).FirstOrDefault();
            if (bilgiler != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,a.KulAdi)
                };
                var useridentity = new ClaimsIdentity(claims, "Account");
                ClaimsPrincipal principal = new ClaimsPrincipal(useridentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Account");

            }

            return View();
        }
    }
}
