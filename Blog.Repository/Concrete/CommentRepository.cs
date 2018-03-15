using Blog.Core.Concrete;
using Blog.DAL;
using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Repository.Concrete
{
   public class CommentRepository :EFRepositoryBase<Comment>,ICommentsRepository
    {
        public CommentRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
