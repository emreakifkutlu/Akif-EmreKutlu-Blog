using Blog.BLL.Services.Abstract;
using Blog.BLL.Services.Concrete;
using Blog.BLL.Validations.LoginValidations;
using Blog.BLL.Validations.LoginValidator;
using Blog.DAL;
using Blog.DAL.Entities;
using Blog.Repository.UOW.Abstract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace BlogProject.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IMailMessage _mesaj;
        private readonly IEncryptor _encrypt;
        //bool IsSuccess;
        //string _kod;

        public AccountController(IUnitOfWork uow, IEncryptor encrypt,IMailMessage mesaj)
        {
            _uow = uow;
            _mesaj = mesaj;
            _encrypt = encrypt;
        }
        // GET: Admin/Account
       

        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            else
            {
                return View();
            }
        }   

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User model)
        {
            var validator = new BaseValidator().Validate(model);
            if (validator.IsValid)
            {
                model.Password = _encrypt.Hash(model.Password);

                User loginUser =
                 _uow
                 .GetRepo<User>()
                 .Where(x => x.Email == model.Email && x.Password == model.Password)
                 .FirstOrDefault();

                if (loginUser != null && loginUser.RoleId==1)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, model.RememberMe);
                    return RedirectToAction("Index", "Dashboard");
                }

               else
                {
                    ViewBag.Msg = "E-posta ya da şifre hatalı!";
                    return View();
                }
            }
            else
            {
                validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
            }
            return View();
        }


        //[AllowAnonymous]
        //public ActionResult Register()
        //{
        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //[AllowAnonymous]
        //public ActionResult Register(User model)
        //{
        //    var validator = new UserValidator().Validate(model);
        //    bool sorgu = _uow.GetRepo<User>()
        //        .Any(x => x.Email == model.Email);

        //    IsSuccess = false;

        //    if (validator.IsValid)
        //    {
        //        if (!sorgu)
        //        {

        //            _kod = Guid.NewGuid().ToString();
        //            model.Kod = _kod;
        //            string url = Path.Combine("http://localhost:52502/Admin/Account/RegisterConfirm/", "?kod=" + _kod);
        //            string htmlString = "<a href=" + url + ">Üyeliğimi Doğrula</a>";

        //            var message = (MailMessage)_mesaj;
        //            message.To = model.Email;
        //            bool Ok = message.SendMessage("Üyelik Doğrulama Kodu", htmlString);

        //            ModelState.Clear();

        //            if (Ok)
        //            {
        //                IsSuccess = true;
        //                ViewBag.IsSuccess = IsSuccess;
        //                ViewBag.Msg = "E-posta başarıyla gönderilmiştir";
        //            }
        //            else
        //            {
        //                IsSuccess = false;
        //                ViewBag.IsSuccess = false;
        //                ViewBag.Msg = "E-posta gönderilirken bir hata oluştu,lütfen tekrar deneyin!";
        //            }

        //            string pass = _encrypt.Hash(model.Password);
        //            model.Password = pass;
        //            model.PasswordConfirm = pass;
        //            model.RoleId = 1;
        //            _uow.GetRepo<User>().Add(model);
        //            _uow.Commit();
        //        }
        //        else
        //        {
        //            ViewBag.Msg = "Bu e-posta adresine ait bir kullanıcı zaten mevcut!";
        //        }
        //    }
        //    else
        //    {
        //        validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
        //    }
        //    return View();
        //}

        //public ActionResult RegisterConfirm(string kod)
        //{
        //    int userId = _uow.GetRepo<User>()
        //        .Where(x => x.Kod == kod)
        //        .FirstOrDefault()
        //        .Id;
        //    if (userId != 0)
        //    {
        //        _uow.GetRepo<User>()
        //        .GetById(userId)
        //        .IsAccepted = true;
        //        _uow.Commit();
        //        return RedirectToAction("Login", "Account");
        //    }
        //    return View();
        //}

        //[AllowAnonymous]
        //public ActionResult ForgotPassword()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult ForgotPassword(User model)
        //{
        //    var validator = new ForgotPasswordValidator().Validate(model);
        //    if (validator.IsValid)
        //    {
        //        int forgotPasswordUserId = _uow.GetRepo<User>()
        //            .Where(x => x.Email == model.Email)
        //            .FirstOrDefault()
        //            .Id;

        //        if (forgotPasswordUserId != 0)
        //        {
        //            _kod = Guid.NewGuid().ToString();

        //            _uow.GetRepo<User>()
        //                .GetById(forgotPasswordUserId)
        //                .Kod = _kod;
        //            _uow.Commit();

        //            string url = Path.Combine("http://localhost:52502/Admin/Account/PasswordConfirm/", "?kod=" + _kod);
        //            string htmlString = "<a href=" + url + ">Parolamı sıfırla</a>";

        //            var message = (MailMessage)_mesaj;
        //            message.To = model.Email;
        //            bool Ok = message.SendMessage("Şifre Sıfırlama Talimatı", htmlString);

        //            ModelState.Clear();
        //            if (Ok)
        //            {
        //                IsSuccess = true;
        //                ViewBag.IsSuccess = IsSuccess;
        //                ViewBag.Msg = "E-postanıza onay linki gönderilmiştir";
        //            }
        //            else
        //            {
        //                IsSuccess = false;
        //                ViewBag.IsSuccess = IsSuccess;
        //                ViewBag.Msg = "E-posta gönderilirken bir hata oluştu,lütfen tekrar deneyiniz!";
        //            }
        //            return View();
        //        }
        //        IsSuccess = false;
        //        ViewBag.Msg = "Böyle bir kayıt bulunamadı!";
        //        return View();
        //    }
        //    else
        //    {
        //        validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
        //        return View();
        //    }
        //}

        //public ActionResult PasswordConfirm(string kod)
        //{
        //    User model = new User();
        //    //User passwordConfirm = _uow.GetRepo<User>()
        //    //    .Where(x => x.Kod == kod)
        //    //    .FirstOrDefault();
        //    //if (passwordConfirm != null)
        //    //{
        //    //    model.Id = passwordConfirm.Id;
        //    //    model.Email = passwordConfirm.Email;
        //    //}

        //    model.Email = _uow.GetRepo<User>()
        //        .Where(x => x.Kod == kod)
        //        .FirstOrDefault().Email;

        //    model.Id = _uow.GetRepo<User>()
        //       .Where(x => x.Kod == kod)
        //       .FirstOrDefault().Id;

        //    return View(model);

        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult PasswordConfirm(User model)
        //{
        //    //User passwordConfirm = _uow.GetRepo<User>()
        //    //    .Where(x => x.Email == model.Email)
        //    //    .FirstOrDefault();
        //    //if (passwordConfirm != null)
        //    //{
        //    //    model.Id = passwordConfirm.Id;
        //    //}
        //    var validator = new PasswordConfirmValidator().Validate(model);
        //    if (validator.IsValid)
        //    {
        //        var user = _uow.GetRepo<User>().GetById(model.Id);
               
        //        string pass = _encrypt.Hash(model.Password);
        //        user.Password = pass;
        //        user.PasswordConfirm = pass;
        //        user.RoleId = 1;
        //        _uow.GetRepo<User>().Update(user);


        //        if (_uow.Commit() > 0)
        //        {
        //            IsSuccess = true;
        //            ViewBag.IsSuccess = IsSuccess;
        //            ViewBag.Msg = "Şifre başarıyla değiştirilmiştir.";
        //        }
        //        else
        //        {
        //            IsSuccess = false;
        //            ViewBag.IsSuccess = IsSuccess;
        //            ViewBag.Msg = "Şifre değiştirme işlemi başarısız,tekrar deneyiniz";
        //        }
               
        //        return View();
        //    }
        //    else
        //    {
        //        validator.Errors.ToList().ForEach(x => ModelState.AddModelError(x.PropertyName, x.ErrorMessage));
        //        //ModelState.Clear();
        //        return View();
        //    }
        //}

        [Authorize]
        public RedirectResult LogOut()
        {
            //cookie siler
            FormsAuthentication.SignOut();

            return Redirect("/Admin/Account/Login");
        }
    }

    }
