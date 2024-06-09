using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class SubeController : Controller
    {
        private readonly DataContext dataContext;

        public SubeController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        public IActionResult Index()
        {

            #region DbSorgu
            //  var product = dataContext.ProductTbl.ToList();
            //  var rez = dataContext.ReservationTbl.ToList();
            //  
            //  var onayList = dataContext.ReservationTbl.Select(x => x.Onay).ToList();
            // var rezCon = dataContext.ReservationTbl;
            //var subeId =   sessionUser.SubeId;
            //  var subeName = sessionUser.Name;
            #endregion

            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Sube"));
            var subeAd = dataContext.SubeTbl.Where(x => x.Id == sessionUser.SubeId).Select(x => x.Name).FirstOrDefault();
            var rezContext = dataContext.ReservationTbl.Where(s => s.HangiSube == subeAd).Select(x => x.Product).FirstOrDefault();
            var rezId = dataContext.ReservationTbl.Where(r => r.HangiSube == subeAd).Select(x => x.Id).FirstOrDefault();
            var reservedProductName = dataContext.ProductTbl.Where(p => p.Id == rezContext).Select(x => x.Name).FirstOrDefault();

            List<SubeViewModel> subeView = new List<SubeViewModel>();

            var subeProduct = (from c in dataContext.ReservationTbl
                               select new SubeViewModel()
                               {
                                   ReservedId = c.Id,
                                   SubeId = c.Id,
                                   statu = c.Onay,
                                   SubeName = c.HangiSube,
                                   Products = (from p in dataContext.ProductTbl
                                               where p.SubeId == c.Id
                                               select new Product
                                               {
                                                   Id = p.Id,
                                                   Name = p.Name,
                                                   Description = p.Description,
                                                   ImageUrl = p.ImageUrl,
                                                   Price = p.Price,

                                               }).ToList()
                               }).ToList();



            foreach (var item in subeProduct)
            {
               string name = item.Products.Select(n => n.Name).ToList().FirstOrDefault();
                item.reservedname = name;
                subeView.Add(item);
            }

            return View(subeView);
        }
        [HttpPost]
        public IActionResult Index(int rezId, int productId, int statu,string Name)
        {
            
            var rez = dataContext.ReservationTbl.Where(x => x.Id == rezId);
            ReservationViewModel viewModel = new ReservationViewModel();
            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Sube"));
            var OrderContex = dataContext.OrderTlb.Where(o => o.ProductId == productId);
            Order order = new Order();

            if (statu == 1)
            {
                viewModel.ProductId = productId;
                viewModel.SubeId = (int)sessionUser.SubeId;
                viewModel.Date = rez.Select(x => x.Tarih).FirstOrDefault();
                viewModel.Onay = 1;

                Reservation reservation = new Reservation();
                reservation.Id = rezId;
                reservation.Product = productId;
                reservation.Tarih = rez.Select(x => x.Tarih).FirstOrDefault();
                reservation.Onay = 1;
                reservation.HangiSube = sessionUser.Name;
                reservation.User = (int)sessionUser.SubeId;
                


                order.SubeId = (int)sessionUser.SubeId;
                order.ProductId = productId;
                order.Price = OrderContex.Select(x => x.Price).FirstOrDefault();
                order.UserId = sessionUser.Id;
                order.Unit = 1;
                dataContext.ReservationTbl.Update(reservation);
                dataContext.OrderTlb.Add(order);
                dataContext.SaveChanges();
            };
            if (statu == 2)
            {
                viewModel.ProductId = productId;
                viewModel.SubeId = (int)sessionUser.SubeId;
                viewModel.Date = rez.Select(x => x.Tarih).FirstOrDefault();
                viewModel.Onay = 2;


                Reservation reservation = new Reservation();
                reservation.Id = rezId;
                reservation.Product = productId;
                reservation.Tarih = rez.Select(x => x.Tarih).FirstOrDefault();
                reservation.Onay = 2;
                reservation.HangiSube = sessionUser.Name;
                reservation.User = (int)sessionUser.SubeId;
                dataContext.Update(reservation);

                dataContext.SaveChanges();
            };

            return RedirectToAction("Index", "Sube");
        }
    }
}

