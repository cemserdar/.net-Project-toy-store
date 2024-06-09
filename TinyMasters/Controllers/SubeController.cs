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
            var sessionUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("Sube"));
            var subeAd = dataContext.SubeTbl.Where(x => x.Id == sessionUser.SubeId).Select(x => x.Name).FirstOrDefault();
            var rezContext = dataContext.ReservationTbl.Where(s => s.HangiSube == subeAd).Select(x => x.Product).FirstOrDefault();
            var reservedProductName = dataContext.ProductTbl.Where(p => p.Id == rezContext).Select(x => x.Name).FirstOrDefault();
            var product = dataContext.ProductTbl.ToList();
            var rez = dataContext.ReservationTbl.ToList();
            var rezId = dataContext.ReservationTbl.Where(r => r.HangiSube == subeAd).Select(x => x.Id).FirstOrDefault();
            #endregion
            var onayList = dataContext.ReservationTbl.Select(x => x.Onay).ToList();
            List<SubeViewModel> subeView = new List<SubeViewModel>();
            for (int i = 0; i < rez.Count; i++)
            {
                SubeViewModel sube = new SubeViewModel()
                {
                    SubeId = (int)sessionUser.SubeId,
                    Products = product,
                    SubeName = subeAd,
                    reservedname = reservedProductName,
                    statu = onayList[i],
                    ReservedId = rezId
                };


                subeView.Add(sube);
            }


            return View(subeView);
        }
        [HttpPost]
        public IActionResult Index(int rezId, int productId, int statu)
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
                dataContext.Update(reservation);

                order.SubeId = (int)sessionUser.SubeId;
                order.ProductId = productId;
                order.Price = OrderContex.Select(x => x.Price).FirstOrDefault();
                order.UserId = sessionUser.Id;
                order.Unit = 1;

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
                dataContext.ReservationTbl.Update(reservation);

                dataContext.SaveChanges();
            };

            return RedirectToAction("Index", "Sube");
        }
    }
}

