using E.Comerce.DataAccessLayer.Data;
using E.Comerce.DataAccessLayer.Infrastructer.Irepository;
using E.Comerce.Models;
using E.Comerce.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Timers;

namespace E.commerce.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private IWebHostEnvironment _hostEnvironment;

        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        #region
        public IActionResult AllProducts()
        {
            var products = _unitOfWork.Product.GetAll(includeProperties:"Category");
             return Json(new { data = products });
        }

        #endregion

        public IActionResult Index()
        {

            //ProductVM productVM = new ProductVM();
            //productVM.products  = _unitOfWork.Product.GetAll();
            return View();
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
            ProductVM vm = new ProductVM()
            {
                Product = new(),
                categories = _unitOfWork.Category.GetAll().Select(x =>
                new SelectListItem()
                {
                    Text=x.Name,
                    Value= x.Id.ToString(),
                })
            };
            


            if (id == null || id == 0)
            {
                return View(vm);
            }
            else
            {
                vm.Product = _unitOfWork.Product.GetT(x => x.Id == id);
                if (vm.Product == null)
                {
                    return NotFound();
                }
                else { 
                    return View(vm);
                }
                
            }

        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUpdate(ProductVM vm , IFormFile? file)
        {
            if (ModelState.IsValid)
            {

                string fileName = string.Empty;
                if (file!= null)
                {
                    string uploadDir = Path.Combine(_hostEnvironment.WebRootPath, "ProductImage");
                    fileName = Guid.NewGuid().ToString()+"-"+ file.FileName;
                    string filePath= Path.Combine(uploadDir, fileName);
                    if (vm.Product.ImageUrl != null)
                    {
                        var oldimagepath = Path.Combine(_hostEnvironment.WebRootPath, vm.Product.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldimagepath))
                        {
                            System.IO.File.Delete(oldimagepath);
                        }
                    }


                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);    
                    }
                    vm.Product.ImageUrl = @"\ProductImage\" + fileName;
                }
                if (vm.Product.Id==0)
                {
                   _unitOfWork.Product.Add(vm.Product);
                    TempData["success"] = "Category created Done !";

                }
                else
                {
                    _unitOfWork.Product.Update(vm.Product);
                    TempData["success"] = "Category Updated Done !";

                }
               
              

                _unitOfWork.save();
               
                return RedirectToAction("Index", "Product");
            }
            return RedirectToAction("Index", "Product");
        }


        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    var category = _unitOfWork.Category.GetT(x => x.Id == id);
        //    if (category == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(category);



        #region    Delete APICALL
        [HttpDelete]
      
        public IActionResult Delete(int? id)
        {
            var Product = _unitOfWork.Product.GetT(x => x.Id == id);
            if (Product  == null)
            {
                return Json(new { success = false, Message = "Error in fetching data" });
            }
            else
            {
                var oldimagepath = Path.Combine(_hostEnvironment.WebRootPath, Product.ImageUrl.TrimStart('\\'));
                if (System.IO.File.Exists(oldimagepath))
                {
                    System.IO.File.Delete(oldimagepath);
                }
                _unitOfWork.Product.Delete(Product);
                _unitOfWork.save();
                return Json(new { success = true, Message = "Product deleted done" });
            }



           
           
        }

        #endregion
    }
}
