using E.Comerce.DataAccessLayer.Infrastructer.Irepository;
using E.Comerce.DataAccessLayer.Infrastructer.Repository;
using E.Comerce.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace E.comerce.Web.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private IUnitOfWork _unitOfWork;

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            CartVM itemslist = new CartVM()
            {
                ListOfCart = _unitOfWork.Cart.GetAll(x => x.ApplicationUserId == claims.Value , includeProperties:"Product")
            };
            foreach (var item in itemslist.ListOfCart) 
            {
                itemslist.Total += (item.Product.Price *item.count);
            }
            return View(itemslist);
        }

        public IActionResult plus(int id)
        {
            var Cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            _unitOfWork.Cart.IncreamentCartItem(Cart , 1);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult minus(int id)
        {
            var Cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            if (Cart.count <= 1)
            {
                _unitOfWork.Cart.Delete(Cart);

            }
            else
            {
                 _unitOfWork.Cart.DecrementtCartItem(Cart, 1);    
            }
           
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult delete(int id) 
        {
            var Cart = _unitOfWork.Cart.GetT(x => x.Id == id);
            _unitOfWork.Cart.Delete(Cart);
            _unitOfWork.save();
            return RedirectToAction(nameof(Index));
        }




    }

}
