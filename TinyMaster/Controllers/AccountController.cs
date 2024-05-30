using Microsoft.AspNetCore.Mvc;
using TinyMaster.Models;

namespace TinyMaster.Controllers
{
    public class AccountController : Controller
    {
        private readonly TinyMasterDbContext _context;

        public AccountController(TinyMasterDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            int musteriId = 1; // Bu örnekte müşteri id'si sabit; oturumdan alınmalı
            var musteri = _context.Musteriler.Find(musteriId);
            return View(musteri);
        }
    }
}
