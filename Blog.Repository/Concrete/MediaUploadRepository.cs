using Blog.Core.Concrete;
using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Blog.Repository.Concrete
{
    public class MediaUploadRepository : EFRepositoryBase<MediaUpload>, IMediaUploadRepository
    {
        public MediaUploadRepository(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
