using Blog.BLL.Validations.CategoryValidations;
using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using Blog.Repository.UOW.Abstract;
using BlogProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Areas.Admin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uow;
        bool IsSuccess;
        private readonly ICategoryRepository _categoryRepo;


        public CategoryController(IUnitOfWork uow,ICategoryRepository categoryRepo)
        {
            _uow = uow;
            _categoryRepo = categoryRepo;
        }
        // GET: Admin/Category

        public ActionResult Add()
        {
            
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Category model)
        {
            bool IsSuccess = false;
            var validator = new CategoryAddValidator().Validate(model);
            if (validator.IsValid)
            {
                _uow.GetRepo<Category>()
                    .Add(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori başarıyla eklendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori eklerken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();

        }
        public ActionResult List()
        {
            var model = _uow.GetRepo<Category>().GetList();
            return View(model);
        }

       
        public ActionResult Delete(int id)
        {
          
            _uow.GetRepo<Category>().Delete(id);
            _uow.Commit();
            return RedirectToAction("List","Category");
        }
        public ActionResult Update(int id)
        {

           var model= _uow.GetRepo<Category>().GetObject(x=>x.Id==id);
           
            return View(model);
        }
        [HttpPost][ValidateAntiForgeryToken]
        public ActionResult Update(Category model)
        {

            var validator = new CategoryEditValidator(_categoryRepo).Validate(model);
            if (validator.IsValid)
            {
                _uow.GetRepo<Category>()
                    .Update(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori başarıyla güncellendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kategori güncellerken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();
        }

    }
}