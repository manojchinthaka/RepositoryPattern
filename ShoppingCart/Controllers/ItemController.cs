using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Repository;
using EntityCore;

 
namespace ShoppingCart.Controllers
{
   
   
    public class ItemController : Controller
    {
        private UnitOfWork _uow = new UnitOfWork();
        // GET: Item
        public ActionResult Index()
        {
 
            List<ItemCategoryMasterVM> _masterVM = new List<ItemCategoryMasterVM>();
            var itemList = _uow.Repository<ItemMaster>().Get();
            
            foreach(var item in itemList)
            {
                ItemCategoryMasterVM objMasterCategory = new ItemCategoryMasterVM();
                objMasterCategory.Id = item.Id;
                objMasterCategory.ItemCode = item.ItemCode;
                objMasterCategory.Description = item.Description;
                objMasterCategory.Price = item.Price;
                objMasterCategory.CategoryName = _uow.Repository<ItemCategory>().GetById(item.ItemCategoryId).Name;
                _masterVM.Add(objMasterCategory);
            }

       
            return View(_masterVM);

            //List<ItemMaster> _master = new List<ItemMaster>();
            //var itemList = _uow.Repository<ItemMaster>().Get();

            //ItemMaster objMasterCategory = new ItemMaster();
            //foreach (var item in itemList)
            //{
            //    objMasterCategory.ItemCode = item.ItemCode;
            //    objMasterCategory.Description = item.Description;
            //    objMasterCategory.Price = item.Price;
            //    objMasterCategory.ItemCategory.Name = _uow.Repository<ItemCategory>().GetById(item.ItemCategoryId).Name;
            //    _master.Add(objMasterCategory);
            //}

            //return View(_master);
        }
       public ActionResult Details(Guid id)
        {
            ItemCategoryMasterVM _itMasterVM = new ItemCategoryMasterVM();
            var item = _uow.Repository<ItemMaster>().GetById(id);
            _itMasterVM.ItemCode = item.ItemCode;
            _itMasterVM.Description = item.Description;
            _itMasterVM.Price = item.Price;

            _itMasterVM.CategoryName = _uow.Repository<ItemCategory>().GetById(item.ItemCategoryId).Name;
            return View(_itMasterVM);
        }
        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            ItemCategoryMasterVM _itMasterVM = new ItemCategoryMasterVM();
            var item = _uow.Repository<ItemMaster>().GetById(id);
            _itMasterVM.ItemCode = item.ItemCode;
            _itMasterVM.Description = item.Description;
            _itMasterVM.Price = item.Price;
            _itMasterVM.CategoryId = item.ItemCategoryId;

            var itemCategory = _uow.Repository<ItemCategory>().Get().Select(x=>new SelectListItem { Value=x.Id.ToString() ,Text=x.Name}).ToList();
            ViewBag.itemCategory = itemCategory;

            return View(_itMasterVM);
        }


        [HttpPost]
        public ActionResult Edit(ItemCategoryMasterVM _itemVW)
        {
            ItemMaster _itMaster = new ItemMaster();

            _itMaster.Id = _itemVW.Id;
            _itMaster.ItemCode = _itemVW.ItemCode;
            _itMaster.Description = _itemVW.Description;
            _itMaster.Price = _itemVW.Price;
            _itMaster.ItemCategoryId = _itemVW.CategoryId;

            var itemCategory = _uow.Repository<ItemCategory>().Get().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.itemCategory = itemCategory;

            _uow.Repository<ItemMaster>().Update(_itMaster);
            _uow.Save();
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            var itemCategory = _uow.Repository<ItemCategory>().Get().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.itemCategory = itemCategory;

            return View();
        }
        [HttpPost]
        public ActionResult Create(ItemCategoryMasterVM _vmMster)
        {

            var itemCategory = _uow.Repository<ItemCategory>().Get().Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();
            ViewBag.itemCategory = itemCategory;

            ItemMaster _masterItem = new ItemMaster();
            _masterItem.Id = Guid.NewGuid();
            _masterItem.ItemCode = _vmMster.ItemCode;
            _masterItem.Description = _vmMster.Description;
            _masterItem.Price = _vmMster.Price;
            _masterItem.ItemCategoryId = _vmMster.CategoryId;

             _uow.Repository<ItemMaster>().Add(_masterItem);
            _uow.Save();
            return View();
        }
      
        public ActionResult Delete(Guid id)
        {
            var item = _uow.Repository<ItemMaster>().GetById(id);

            _uow.Repository<ItemMaster>().Delete(item.Id);
            _uow.Save();
          return  RedirectToAction("Index");
        } 
    }
}