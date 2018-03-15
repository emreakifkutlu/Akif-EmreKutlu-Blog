using Blog.Core.Abstract;
using Blog.DAL;
using Blog.DAL.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Abstract
{
   public  interface ICategoryRepository :IRepository<Category>
    {
    }
}
