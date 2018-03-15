using Blog.BLL.Validations.CommentValidations;
using Blog.DAL.Entities;
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
    public class CommentController : Controller
    {
      private readonly  IUnitOfWork _uow;
        // GET: Admin/Comment

        public CommentController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public ActionResult List()
        {
            var model = _uow.GetRepo<Comment>().Where(x=>x.IsDeleted==false);
            return View(model);
        }

        public ActionResult Add()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Comment model)
        {
            bool IsSuccess = false;
            var validator = new CommentAddValidator().Validate(model);
           
            if (validator.IsValid)
            {
                model.LikeCount = 0;
                model.DislikeCount = 0;
                model.CommentDate = DateTime.Today;
                model.UserId = 2;
                _uow.GetRepo<Comment>()
                    .Add(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yorum başarıyla eklendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yorum eklerken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();
        }

        public ActionResult Update(int id)
        {
            var model = _uow.GetRepo<Comment>().GetObject(x=>x.Id==id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(Comment model)
        {
            bool IsSuccess=false;
            var validator = new CommentEditValidator().Validate(model);
            if (validator.IsValid)
            {
                model.CommentDate = DateTime.Today;
                _uow.GetRepo<Comment>()
                    .Update(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yorum başarıyla güncellendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Yorum güncellenirken bir hata oluştu!";
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
           

            return View();
        }

        public ActionResult Delete(int id)
        {
            _uow.GetRepo<Comment>().Delete(id);
            _uow.Commit();
            return RedirectToAction("List", "Comment");
        }


        //public ActionResult Update(int id)
        //{
        //    var model = _uow.GetRepo<Comment>().GetObject(x => x.Id == id);
        //    return View(model);
        //}

    }
}