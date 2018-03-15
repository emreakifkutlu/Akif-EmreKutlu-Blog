using Blog.DAL;
using Blog.DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.LoginValidator
{
   public class ForgotPasswordValidator : AbstractValidator<User>
    {
        public ForgotPasswordValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-postanızı giriniz");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Epostanızı giriniz");
        }
    }
}
