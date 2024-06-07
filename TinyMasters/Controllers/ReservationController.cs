using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class ReservationController : Controller
    {
        public readonly DataContext _context;

        public ReservationController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index(int ProductId)
        {
            ReservationViewModel model = new ReservationViewModel();
            var db = _context.ProductTbl.Where(d=>d.Id == ProductId);
            
            model.ProductId = ProductId;
            model.SubeId = db.Select(s => s.SubeId).FirstOrDefault();
            model.SubeAdi = _context.SubeTbl.Select(s=>s.Name).ToList();
            model.KisiSayisi = _context.UserTbl.Select(p => p.SubeId).Count();
            
            Reservation reservation = new Reservation();
            reservation.Product = model.ProductId;
            reservation.HangiSube = model.SubeAdi.ToString();
            reservation.Tarih = model.Date;
            reservation.User = model.UserId;
            
            

            
            return View(model);
        }


        [HttpPost]
        public IActionResult Index(DateTime Date, int kisisayisi, string SubeAdi)
        {
            var context = _context.ReservationTbl;
           
            return View();
        }
    }
}
