using Blog.DAL;
using Blog.DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.LoginValidations
{
  public  class BaseValidator :AbstractValidator<User>
    {
        public BaseValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş geçilemez!");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez!");
        }
    }
}
