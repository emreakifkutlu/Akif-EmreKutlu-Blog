using Blog.BLL.Validations.LoginValidator;
using Blog.DAL.Entities;
using Blog.DAL.Model;
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
    public class UserController : Controller
    {
        private readonly IUnitOfWork _uow;
        public UserController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Admin/User
        public ActionResult List()
        {
            var model = _uow.GetRepo<User>().GetList().ToList();
          
            return View(model);
        }

        public ActionResult Add()
        {
          
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(User model)
        {
            bool IsSuccess = false;
            var validator = new UserValidator().Validate(model);
            if (validator.IsValid)
            {
                _uow.GetRepo<User>()
                    .Add(model);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kullanıcı başarıyla eklendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Kullanıcı eklenirken bir hata oluştu!";
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