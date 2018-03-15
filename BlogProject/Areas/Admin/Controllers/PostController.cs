using Blog.BLL.Validations.PostValidations;
using Blog.DAL.Entities;
using Blog.DAL.Model;
using Blog.Repository.Abstract;
using Blog.Repository.UOW.Abstract;
using BlogProject.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlogProject.Areas.Admin.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IUnitOfWork _uow;
        private readonly IPostRepository _PostRepo;
        public PostController(IUnitOfWork uow, IPostRepository PostRepo)
        {
            _uow = uow;
            _PostRepo = PostRepo;

        }
        // GET: Admin/Post
        public ActionResult List()
        {
            var model = _uow.GetRepo<Post>().Where(x => x.IsDeleted == false);
            return View(model);
        }

        public ActionResult Add()
        {
            IEnumerable Kategori = _uow.GetRepo<Category>().GetList();
            ViewData["Kategoriler"] = new SelectList(Kategori, "Id", "Name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(PostViewModel model)
        {
            if (model.PostedPic != null)
            {
               
                MediaUpload upload = new MediaUpload();
                upload = UploadToDb(model.PostedPic);
                _uow.GetRepo<MediaUpload>()
                    .Add(upload);
                _uow.Commit();
                model.Post.PostPic = upload.Path.ToString();
              
            }

            IEnumerable Kategori = _uow.GetRepo<Category>().GetList();
            ViewData["Kategoriler"] = new SelectList(Kategori, "Id", "Name");

            bool IsSuccess = false;
            var validator = new PostAddValidator().Validate(model.Post);
            if (validator.IsValid)
            {
                model.Post.PostDate = DateTime.Today;
                model.Post.UserId = 2;
                model.Post.LikeCount = 0;
                model.Post.DislikeCount = 0;
                _uow.GetRepo<Post>()
                    .Add(model.Post);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Post başarıyla eklendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Post eklerken bir hata oluştu!";
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
            var model = _uow.GetRepo<Post>().GetById(id);
            PostViewModel model2 = new PostViewModel();
            model2.Post = model;
            return View(model2);
        }

        [HttpPost] [ValidateAntiForgeryToken]
        public ActionResult Update(PostViewModel model)
        {
             if (model.PostedPic != null)
            {
               
                MediaUpload upload = new MediaUpload();
                upload = UploadToDb(model.PostedPic);
                _uow.GetRepo<MediaUpload>()
                    .Add(upload);
                _uow.Commit();
                model.Post.PostPic = upload.Path.ToString();
              
            }
            bool IsSuccess = false;
            var validator = new PostEditValidator(_PostRepo).Validate(model.Post);
            if (validator.IsValid)
            {
                model.Post.PostDate = DateTime.Today;
                _uow.GetRepo<Post>()
                    .Update(model.Post);
                if (_uow.Commit() > 0)
                {
                    IsSuccess = true;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Post başarıyla güncellendi.";
                }
                else
                {
                    IsSuccess = false;
                    ViewBag.Result = IsSuccess;
                    ViewBag.Msg = "Post güncellenirken bir hata oluştu!";
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

            _uow.GetRepo<Post>().Delete(id);
            _uow.Commit();
            return RedirectToAction("List", "Post");
        }

        MediaUpload UploadToDb(HttpPostedFileBase file)
        {
            string path = "";
            if (file != null && file.ContentLength > 0)
                try
                {
                    //string fullFileName = HttpContext.Server.MapPath("/Media/Images/" + uniqueFileName + extention);
                    path = HttpContext.Server.MapPath("/Media/Images/" + file.FileName);
                    //path = Path.Combine(Server.MapPath("/Media/Images"),Path.GetFileName(PostedPic.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
         
            MediaUpload upload = new MediaUpload();
            upload.Name = file.FileName;
            upload.Path = "/Media/Images/" + file.FileName; ;

            return upload;

        }

        #region DB ye resim yükleme
        public ActionResult UploadImage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UploadImage(HttpPostedFileBase file)
        {

            if (file != null && file.ContentLength > 0)
                try
                {
                    string path = HttpContext.Server.MapPath("/Media/Images/" + file.FileName);
                    //string path = Path.Combine(Server.MapPath("/Media/Images"),
                    //                           Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File uploaded successfully";
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "ERROR:" + ex.Message.ToString();
                }
            else
            {
                ViewBag.Message = "You have not specified a file.";
            }
            return View();
        } 
        #endregion
    }
}