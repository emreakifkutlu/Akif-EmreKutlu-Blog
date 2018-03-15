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
    public class PostSiteController : Controller
    {
       private readonly IUnitOfWork _uow;
        public PostSiteController(IUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

      
        public ActionResult Post(int id)
        {
            SiteHomeViewModel model = new SiteHomeViewModel();
            //var model = _uow.GetRepo<Post>().GetById(id);
            model.Gonderi = _uow.GetRepo<Post>().GetById(id);
            model.h1 = model.Gonderi.Title;
            model.subheading = model.Gonderi.Description;
            model.Url = "url('/Media/Images/post-bg.jpg')";
            model.Yorumlar = _uow.GetRepo<Comment>().Where(x => x.PostId == id);
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                ViewBag.Log = "true";
            }
            else
            {
                ViewBag.Log = "false";
            }
           //if(HttpContext.User.Identity.Name != null)
           // {
           //     model.LogUser = "emre";
           // }
        

            return View(model);
        }

        [HttpPost]
        public JsonResult CommentAdd(string yorum,int postId)
        {

            CommentViewModel c = new CommentViewModel()
            {
                CommentBody = yorum,
                CommentDate = DateTime.Now.ToString(),
                Name = "akif",
                LastName = "kutlu",
                PostId = postId,
            };

            Comment com = new Comment();
            com.CommentBody = c.CommentBody;
            com.CommentDate = DateTime.Now;
            com.PostId = c.PostId;
            com.UserId = 2;
            _uow.GetRepo<Comment>().Add(com);
            _uow.Commit();
            return Json(c);
        }


        [HttpPost]
        public JsonResult Like(int likeCount,int postId)
        {
            _uow.GetRepo<Post>().GetById(postId).LikeCount = likeCount;
            _uow.Commit();
            return Json(likeCount);
        }

        [HttpPost]
        public JsonResult dislike(int dislikeCount, int postId)
        {
            _uow.GetRepo<Post>().GetById(postId).DislikeCount = dislikeCount;
            _uow.Commit();
            return Json(dislikeCount);
        }

    }
}