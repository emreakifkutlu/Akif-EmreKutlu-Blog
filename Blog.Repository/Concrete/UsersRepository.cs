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
   public class UsersRepository : EFRepositoryBase<User>, IUsersRepository
    {
        public UsersRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
