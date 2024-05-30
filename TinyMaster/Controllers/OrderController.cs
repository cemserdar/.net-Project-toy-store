//using Microsoft.AspNetCore.Mvc;
//using TinyMaster.Models;
//using System.Linq;
//using Microsoft.AspNetCore.DataProtection;
//using TinyMaster.Models.Entities;

//namespace TinyMaster.Controllers
//{
//    public class OrderController : Controller
//    {

//        private readonly TinyMasterDbContext _context;

//        public OrderController(TinyMasterDbContext context)
//        {
//            _context = context;
//        }

//        public ActionResult Index()
//        {


//            var sepet = Session["Sepet"] as List<OrderedItemModel>;
//            if (sepet == null)
//            {
//                sepet = new List<OrderedItemModel>();
//                Session["Sepet"] = sepet;
//            }
//            return View(sepet);
//        }

//        public ActionResult AddToCart(int id)
//        {
//            var urun = _context.Urunler.Find(id);
//            if (urun != null)
//            {
//                var sepet = Session["Sepet"] as List<OrderedItemModel>;
//                if (sepet == null)
//                {
//                    sepet = new List<OrderedItemModel>();
//                    Session["Sepet"] = sepet;
//                }
//                var siparisDetay = sepet.FirstOrDefault(u => u.UrunId == id);
//                if (siparisDetay == null)
//                {
//                    siparisDetay = new OrderedItemModel
//                    {
//                        UrunId = urun.Id,
//                        Urun = urun,
//                        Miktar = 1,
//                        Fiyat = urun.Fiyat
//                    };
//                    sepet.Add(siparisDetay);
//                }
//                else
//                {
//                    siparisDetay.Miktar++;
//                }
//            }
//            return RedirectToAction("Index");
//        }

//        public ActionResult Checkout()
//        {
//            var sepet = Session["Sepet"] as List<OrderedItemModel>;
//            if (sepet != null && sepet.Count > 0)
//            {
//                var siparis = new Siparis
//                {
//                    SiparisTarihi = DateTime.Now,
//                    MusteriId = 1, // Bu örnekte müşteri id'si sabit; oturumdan alınmalı
//                    ToplamFiyat = sepet.Sum(s => s.Miktar * s.Fiyat),
//                    SiparisDetaylari = sepet
//                };
//                _context.Siparisler.Add(siparis);
//                _context.SaveChanges();
//                Session["Sepet"] = null;
//                return RedirectToAction("Onay");
//            }
//            return RedirectToAction("Index");
//        }

//        public ActionResult Onay()
//        {
//            return View();
//        }
//    }
//}
