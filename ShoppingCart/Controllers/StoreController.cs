using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityCore;
using Repository;
namespace ShoppingCart.Controllers
{
    public class StoreController : Controller
    {

      
        private UnitOfWork _uow = new UnitOfWork();
        // GET: Store
        public ActionResult Index()
        {
            //ItemCategory category = new ItemCategory();
           var category =_uow.Repository<ItemCategory>().Get();

            return View(category);
        }
        public ActionResult ItemDetails(Guid Id)
        {

            List<ItemCategoryMasterVM> _masterVM = new List<ItemCategoryMasterVM>();
            var itemList = _uow.Repository<ItemMaster>().Get().Where(i=>i.ItemCategoryId==Id);

            foreach (var item in itemList)
            {
                ItemCategoryMasterVM objMasterCategory = new ItemCategoryMasterVM();
                objMasterCategory.Id = item.Id;
                objMasterCategory.ItemCode = item.ItemCode;
                objMasterCategory.Description = item.Description;
                objMasterCategory.Price = item.Price;
                objMasterCategory.ImagePath = item.imagePath;
                objMasterCategory.CategoryName = _uow.Repository<ItemCategory>().GetById(item.ItemCategoryId).Name;
                _masterVM.Add(objMasterCategory);
            }
            return View(_masterVM);
        }
    }
}