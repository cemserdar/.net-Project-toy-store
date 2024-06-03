using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data.Common;
using TinyMaster.Models;
using TinyMaster.Models.Entities;
using TinyMaster.ViewModels;

namespace TinyMaster.Controllers
{

    public class HomeController : Controller
    {
        private readonly TinyMasterDbContext _db;

        public HomeController(TinyMasterDbContext db)
        {
            _db = db;
        }
        [HttpGet]
        public ActionResult Index()
        {

            List<ProductModel> model = new List<ProductModel>();
            foreach (var item in _db.Urunler)
            {

                ProductModel product = new ProductModel
                {
                    Id = item.Id,
                    Isim = item.Isim,
                    Aciklama = item.Aciklama,
                    FotoUrl = item.FotoUrl,
                    Fiyat = item.Fiyat
                };
                model.Add(product);
            }

            List<HomeViewModel> homeViewModel = new List<HomeViewModel>();


            for (int i = 0; i < model.Count(); i++)
            {
                HomeViewModel viewModel = new HomeViewModel();

                viewModel.UrunId = model[i].Id;
                viewModel.UrunAdi = model[i].Isim;
                viewModel.UrunFiyat = model[i].Fiyat;
                viewModel.FotoUrl = model[i].FotoUrl;
                viewModel.Aciklama = model[i].Aciklama;

                homeViewModel.Add(viewModel);
            }

            return View(homeViewModel);
        }

        [HttpPost]
        public ActionResult Index(int UrunId)
        {

            List<ProductModel> orderedItem = new List<ProductModel>();

            var product = _db.Urunler.Where(x => x.Id == UrunId).FirstOrDefault();

            for (int i = 0; i < product.Isim.Count(); i++)
            {
                ProductModel productModel = new ProductModel();
                {
                    productModel.Id = UrunId;
                    productModel.Isim = product.Isim;
                    productModel.Fiyat = product.Fiyat;
                    productModel.FotoUrl = product.FotoUrl;
                }
                orderedItem.Add(productModel);
            }
            return View(orderedItem);
        }
    }
}
