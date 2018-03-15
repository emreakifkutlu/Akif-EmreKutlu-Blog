using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.DAL.Model
{
    public class CommentViewModel
    {
        public string CommentBody { get; set; }
        public int PostId { get; set; }
        public string CommentDate { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
       
    }
}
