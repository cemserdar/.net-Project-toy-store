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
            var queryProductList = _context.ProductTbl.Where(d => d.Id == ProductId);
            var subeIdList = queryProductList.GroupBy(gp => gp.SubeId).Select(gp => gp.Key).ToList();
            var subeList = _context.SubeTbl.Where(s => subeIdList.Contains(s.Id)).Select(s => new SubeViewModel
            {
                SubeId = s.Id,
                SubeName = s.Name
            }).ToList();

            var product = queryProductList.FirstOrDefault();
            var reservedCount = _context.ReservationTbl.Where(r => r.Product == ProductId).Count();

            ReservationViewModel model = new ReservationViewModel()
            {
                ProductId = product.Id,
                SubeList = subeList,
                KisiSayisi = reservedCount,
                Date = DateTime.UtcNow,
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult Index(ReservationViewModel model)
        {
            var context = _context.ReservationTbl;
            Reservation reservation = new Reservation();

            var sube = _context.SubeTbl.Where(s => s.Id == model.SubeId).Select(s => s.Name).FirstOrDefault();
            var product = _context.ProductTbl.Where(s => s.Id == model.ProductId).Select(s => s.Id).FirstOrDefault();

            reservation.HangiSube = sube;
            reservation.Tarih = model.Date;
            reservation.Product = product;
            reservation.User = model.UserId;

            context.Add(reservation);
            _context.SaveChanges();
            return RedirectToAction("Index","Home");
        }
    }
}