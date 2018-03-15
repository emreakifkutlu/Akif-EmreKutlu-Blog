using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.CommentValidations
{
  public  class CommentEditValidator : AbstractValidator<Comment>
    {
        
        public CommentEditValidator( )
        {
            
            RuleFor(x => x.CommentBody).NotEmpty().WithMessage("Yorum alanı boş geçilemez");
            
        }

       
    }
}
