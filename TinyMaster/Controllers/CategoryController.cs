using Microsoft.AspNetCore.Mvc;
using TinyMaster.Models;
using TinyMaster.Models.Entities;
using TinyMaster.ViewModels;

namespace TinyMaster.Controllers
{
	public class CategoryController : Controller
	{

		private readonly TinyMasterDbContext _context;

		public CategoryController(TinyMasterDbContext context)
		{
			_context = context;
		}

		public ActionResult Index()
		{
			var urunler = _context.Urunler.ToList();
			List<ProductModel> model = new List<ProductModel>();
			List<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>();

			foreach (var item in urunler)
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

			ViewBag.count = _context.Urunler.Count();
			return View(model);
		}
	}
}
