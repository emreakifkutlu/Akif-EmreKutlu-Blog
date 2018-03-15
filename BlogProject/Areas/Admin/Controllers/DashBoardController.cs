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
    public class DashBoardController : Controller
    {
       
        private readonly IUnitOfWork _uow;

        public DashBoardController(IUnitOfWork uow)
        {
            _uow = uow;   
        }
        // GET: Admin/DashBoard
        public ActionResult Index()
        {
            
            var model = _uow.GetRepo<Message>().GetList().ToList();
            return View(model);

        }
    }
}