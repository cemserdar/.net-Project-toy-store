using Microsoft.AspNetCore.Mvc;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class CategoryController : Controller
    {
        private readonly DataContext _dataContext;

        public CategoryController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public IActionResult Index()
        {
            List<Category> category = new List<Category>();
            var product = _dataContext.ProductTbl.ToList();
            foreach (var item in _dataContext.ProductTbl)
            {
                Category urun = new Category();
                {
                    urun.Id = item.Id;
                    urun.Name = item.Name;
                    urun.Product = product;   
                }
                category.Add(urun);
            }

            List<CategoryViewModel> categoryViewModel = new List<CategoryViewModel>();
            foreach (var item in category)
            {
                CategoryViewModel viewModel = new CategoryViewModel();
                {
                    viewModel.ProductId = item.Id;
                    viewModel.Price = item.Product.Select(x => x.Price).FirstOrDefault();
                    viewModel.Name = item.Name;
                    viewModel.Decsription = item.Product.FirstOrDefault()?.Description;
                    viewModel.PictureUrl = item.Product.FirstOrDefault().ImageUrl;
                }
                categoryViewModel.Add(viewModel);
            }

            
            return View(categoryViewModel);

        }
    }
}
