using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using Blog.Repository.UOW.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.PostValidations
{
  public  class PostEditValidator : AbstractValidator<Post>
    {

        private readonly IPostRepository _postRepo;
        public PostEditValidator(IPostRepository postRepo)
        {
            _postRepo = postRepo;
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bir başlık adı girmelisiniz!");
            RuleFor(x => x.PostBody).NotEmpty().WithMessage("İçerik boş geçilemez!");
            RuleFor(x => x).Must(HasUniqueTitle).WithMessage("Böyle bir başlık önceden girilmiş!");
        }

        public bool HasUniqueTitle(Post model)
        {

            var data = _postRepo.Where(x => x.Title == model.Title && x.Id != model.Id).FirstOrDefault();
            if (data == null)
            {
                return true;
            }
            return false;
        }
    }
}
