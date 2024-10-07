﻿using BookShop.Models;
using BookShop.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using BookShop.DataAccess.Repository;
using Microsoft.AspNetCore.Authorization;
using BookShop.Utility;

namespace BookShopWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {


        // private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            //_categoryRepo = db;
            _unitOfWork = unitOfWork;
        }
    public IActionResult Index()
    {
        //List<Category> objCategoryList = _db.Categories.ToList();
        List<Category> objCategoryList = _unitOfWork.Category.GetAll().ToList();
        return View(objCategoryList);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Category obj)
    {
        //if (obj.Name == obj.DisplayOrder.ToString())
        //{
        //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        //}

        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            //_db.Categories.Add(obj);
            //_db.SaveChanges();

            //_categoryRepo.Add(obj);
            //_categoryRepo.Save();

            _unitOfWork.Category.Add(obj);
            _unitOfWork.Save();

            TempData["success"] = "Category created successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Edit(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }


        // Category? categoryFromDb = _db.Categories.Find(id);            
        //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

        //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost]
    public IActionResult Edit(Category obj)
    {
        if (obj.Name == obj.DisplayOrder.ToString())
        {
            ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
        }
        if (ModelState.IsValid)
        {
            // _db.Categories.Add(obj);
            //_db.Categories.Update(obj);
            //_db.SaveChanges();

            //_categoryRepo.Update(obj);
            //_categoryRepo.Save();

            _unitOfWork.Category.Update(obj);
            _unitOfWork.Save();
            TempData["success"] = "Category updated successfully";
            return RedirectToAction("Index");
        }
        return View();
    }

    public IActionResult Delete(int? id)
    {
        if (id == null || id == 0)
        {
            return NotFound();
        }
        //Category? categoryFromDb = _db.Categories.Find(id);
        //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

        //Category? categoryFromDb = _categoryRepo.Get(u => u.Id == id);

        Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);
        if (categoryFromDb == null)
        {
            return NotFound();
        }
        return View(categoryFromDb);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeletePOST(int? id)
    {
        // Category? obj = _db.Categories.Find(id);

        //Category? obj = _categoryRepo.Get(u => u.Id == id);

        Category? obj = _unitOfWork.Category.Get(u => u.Id == id);
        if (obj == null)
        {
            return NotFound();
        }
        //_db.Categories.Remove(obj);
        //_db.SaveChanges();

        //_categoryRepo.Remove(obj);
        //_categoryRepo.Save();

        _unitOfWork.Category.Remove(obj);
        _unitOfWork.Save();
        TempData["success"] = "Category deleted successfully";
        return RedirectToAction("Index");
    }
}
}
