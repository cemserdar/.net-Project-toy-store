using Microsoft.AspNetCore.Mvc;
using TinyMaster.Models;
using TinyMaster.Models.Entities;
using TinyMaster.ViewModels;

namespace TinyMaster.Controllers
{
    // [Route("api/[controller]")]
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
            List<BranchModel> branchModel = new List<BranchModel>();
            foreach (var item in _db.Subeler)
            {
                BranchModel branch = new BranchModel
                {
                    Id = item.Id,
                    Isim = item.Isim,
                    Adres = item.Adres,
                    Rezervation = item.Rezervation
                };
                branchModel.Add(branch);
            }
            List<HomeViewModel> homeViewModel = new List<HomeViewModel>();

            for (int i = 0; i < model.Count(); i++)
            {
                HomeViewModel viewModel = new HomeViewModel();

                    viewModel.UrunAdi = model[i].Isim;
                    viewModel.UrunFiyat = model[i].Fiyat;
                    viewModel.SubeAdi = branchModel[i].Isim;
                    viewModel.FotoUrl = model[i].FotoUrl;
              

                homeViewModel.Add(viewModel);
            }




            return View(homeViewModel);
        }
    }
}
