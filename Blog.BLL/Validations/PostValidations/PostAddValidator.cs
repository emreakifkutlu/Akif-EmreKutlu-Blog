using Blog.DAL;
using Blog.DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.PostValidations
{
   public class PostAddValidator : AbstractValidator<Post>
    {
        public PostAddValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Bir yazı başlığı giriniz.");
            RuleFor(x => x.PostBody).NotEmpty().WithMessage("Gönderi içeriği boş geçilemez");
        }
    }
}
