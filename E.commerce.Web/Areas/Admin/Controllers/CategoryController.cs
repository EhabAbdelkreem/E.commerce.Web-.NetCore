using E.Comerce.DataAccessLayer.Data;
using E.Comerce.DataAccessLayer.Infrastructer.Irepository;
using E.Comerce.Models;
using E.Comerce.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Timers;

namespace E.commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {

            CategoryVM categoryVM = new CategoryVM();
            categoryVM.categories  = _unitOfWork.Category.GetAll();
            return View(categoryVM);
        }
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    return View();

        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Create(Category category)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Category.Add(category);
        //        _unitOfWork.save();
        //        TempData["success"] = "Category Created Done !";
        //        return RedirectToAction("Index", "Category");
        //    }
        //    return View(category);
        //}
        [HttpGet]
        public IActionResult CreateUpdate(int? id)
        {
            CategoryVM VM = new CategoryVM();   
            if (id == null || id == 0)
            {
                return View(VM);
            }
            else
            {
                VM.Category = _unitOfWork.Category.GetT(x => x.Id == id);
                if (VM.Category == null)
                {
                    return NotFound();
                }
                else { 
                    return View(VM);
                }
                
            }

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(CategoryVM VM)
        {
            if (ModelState.IsValid)
            {
                if (VM.Category.Id == 0)
                {
                    _unitOfWork.Category.Add(VM.Category);
                    TempData["success"] = "Category Created Done !";
                }
                else
                {
                    _unitOfWork.Category.Update(VM.Category);
                    TempData["success"] = "Category Updated Done !";
                }
               
                _unitOfWork.save();
               
                return RedirectToAction("Index", "Category");
            }
            return RedirectToAction("Index", "Category");
        }


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);

        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteData(int? id)
        {
            var Category = _unitOfWork.Category.GetT(x => x.Id == id);
            if (Category == null) { return NotFound(); }
            _unitOfWork.Category.Delete(Category);
            _unitOfWork.save();
            TempData["success"] = "Category Deletd Done !";
            return RedirectToAction("Index", "Category");
        }


    }
}
