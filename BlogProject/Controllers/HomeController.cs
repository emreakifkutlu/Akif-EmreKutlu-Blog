using Blog.BLL.Services.Abstract;
using Blog.DAL.Entities;
using Blog.DAL.Model;
using Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class HomeController : Controller
    {
        IEncryptor _encrypt;
        IUnitOfWork _uow;
        public HomeController(IUnitOfWork uow, IEncryptor encrypt)
        {
            _uow = uow;
            _encrypt = encrypt;
            
           
        }

        public ActionResult Index()
        {
            SiteHomeViewModel model = new SiteHomeViewModel();
            //var model = _uow.GetRepo<Post>().GetList();
            model.Gonderiler= _uow.GetRepo<Post>().GetList();
            model.h1 = "Clean Blogggg";
            model.subheading = "A Blog Theme by Start Bootstrap";
            model.Url = "url('/Media/Images/home-bg.jpg')";
            return View(model);
        }

        



    }
}