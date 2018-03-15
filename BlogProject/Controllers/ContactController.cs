using Blog.BLL.Validations.MessageValidations;
using Blog.DAL.Entities;
using Blog.DAL.Model;
using Blog.Repository.Abstract;
using Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Controllers
{
    public class ContactController : Controller
    {
        private readonly IUnitOfWork _uow;
        
       
        public ContactController(IUnitOfWork uow)
        {
            _uow = uow;
           
        }
        // GET: Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Index(Message model)
        {
            if (model != null)
            {
                var validator = new MessageValidator().Validate(model);
                if (validator.IsValid)
                {
                    User k = _uow.GetRepo<User>()
                        .Where(x => x.Email == model.Email)
                        .FirstOrDefault();
                    if (k == null)
                    {
                      
                        model.MessageDate = DateTime.Now;
                        model.MessageFrom = "Misafir";
                    }
                    else
                    {
                        model.Email = k.Email;
                        model.MessageDate = DateTime.Now;
                        model.UserId = k.Id;
                        model.MessageFrom = k.Name + k.LastName;
                    }
                    _uow.GetRepo<Message>().Add(model);
                    if (_uow.Commit() > 0)
                    {
                        ModelState.Clear();
                        ViewBag.Msg = "Mesajınız başarıyla gönderildi";
                    }



                }
            }
            return View();
        }
    }
}