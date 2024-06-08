using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.VisualStudio;
using TinyMasters.Models;
using TinyMasters.Models.Entity;
using TinyMasters.ViewModel;

namespace TinyMasters.Controllers
{
    public class UserController : Controller
    {

        private readonly DataContext _dataContext;

        public UserController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IActionResult User(User user)
        {

            List<Order> order = new List<Order>();
            List<Reservation> reservation = new List<Reservation>();


            if (user.Role == 1)
            {
                var userContex = _dataContext.UserTbl.Where(x => x.Id == user.Id);
                var orderContex = _dataContext.OrderTlb;
                var productContext = _dataContext.ProductTbl;
                var ProductId = orderContex.Where(x => x.UserId == user.Id).Select(x => x.ProductId).FirstOrDefault();
                var reserved = _dataContext.ReservationTbl.Where(x => x.User == user.Id).Select(x=>x.Product).FirstOrDefault();



                user.Adress = userContex.Select(x => x.Adress).FirstOrDefault();
                user.Name = userContex.Select(x => x.Name).FirstOrDefault();
                user.SubeId = userContex.Select(x => x.SubeId).FirstOrDefault();


                var product = productContext.Where(x => x.Id == orderContex.Where(x => x.UserId == user.Id).Select(x => x.ProductId).FirstOrDefault());

                var statu = 1;
                if (orderContex.Select(x=>x.Unit).FirstOrDefault() == 0)
                {
                   statu = 2;
                }

                UserViewModel userViewModel = new UserViewModel();
                foreach (var item in userContex)
                {
                    userViewModel.UserName = item.Name;
                    userViewModel.ProductPrice = orderContex.Where(x => x.UserId == user.Id).Select(x => x.Price).FirstOrDefault();
                    userViewModel.ProductName = productContext.Where(x => x.Id == ProductId).Select(x => x.Name).FirstOrDefault();
                    userViewModel.ProductImage = productContext.Where(x => x.Id == ProductId).Select(x => x.ImageUrl).FirstOrDefault();
                    userViewModel.ReservedProductImage = productContext.Where(x => x.Id == reserved).Select(x=>x.ImageUrl).FirstOrDefault();
                    userViewModel.ReservedProductName = productContext.Where(x => x.Id == reserved).Select(x => x.Name).FirstOrDefault();
                    userViewModel.ReservedProductStatu = statu;
                }

                return RedirectToAction("User","User",userViewModel);

            }

            if (user.Role == 2)
            {

            }









                return Ok();
        }
    }
}
