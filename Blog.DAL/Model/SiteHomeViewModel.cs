using Blog.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Model
{
public class SiteHomeViewModel
    {
        public IEnumerable<Post> Gonderiler { get; set; }

        public IEnumerable<User> Kullanicilar { get; set; }
        public Post Gonderi { get; set; }
        public User Kullanici { get; set; }
        public IEnumerable<Category> Kategoriler { get; set; }
        public IEnumerable<Comment> Yorumlar { get; set; }

        public IEnumerable<Message> Mesajlar { get; set; }
        public string LogUser { get; set; }
        public string Url { get; set; }
        public string h1 { get; set; }
        public string subheading { get; set; }
    }
}
