using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Blog.DAL.Model
{
   public class PostViewModel
    {
        public Post Post { get; set; }
        public HttpPostedFileBase PostedPic { get; set; }
    }
}
