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
    public class MessageController : Controller
    {
        private readonly IUnitOfWork _uow;
        public MessageController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Admin/Message
        public ActionResult Inbox()
        {
            var model = _uow.GetRepo<Message>().Where(x=>x.IsDeleted==false).ToList();
            return View(model);
        }

      
        public ActionResult Goruntule(int id)
        {
            var model = _uow.GetRepo<Message>().GetById(id);
            return View(model);
        }

       
        public ActionResult Delete(int id)
        {
           _uow.GetRepo<Message>().GetById(id).IsDeleted=true;
            _uow.Commit();
            return RedirectToAction("Inbox","Message");
        }

        public ActionResult Deletedbox()
        {
            var model = _uow.GetRepo<Message>().Where(x=>x.IsDeleted==true).ToList();
            return View(model);
        }
    }
}