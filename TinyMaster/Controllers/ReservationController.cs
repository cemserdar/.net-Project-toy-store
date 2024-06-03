using Microsoft.AspNetCore.Mvc;
using TinyMaster.Models;
using TinyMaster.Models.Entities;

namespace TinyMaster.Controllers
{
    public class ReservationController : Controller
    {

        private readonly TinyMasterDbContext _context;

        public ReservationController(TinyMasterDbContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(RezervationModel urunAyirma)
        {
            if (ModelState.IsValid)
            {
                _context.UrunAyirmalar.Add(urunAyirma);
                _context.SaveChanges();
                return RedirectToAction("Onay");
            }
            return View(urunAyirma);
        }

        public ActionResult Onay()
        {
            return View();
        }
    }
}
