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
 public   class PasswordConfirmValidator : AbstractValidator<User>
    {
        public PasswordConfirmValidator()
        {
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez");
            RuleFor(x => x.Password).Matches(@"^[a-zA-Z0-9]*$").WithMessage("Parolanız en az bir büyük harf ve bir sayı içermelidir.");
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor");
        }
    }
}
