using Blog.DAL;
using Blog.DAL.Entities;
using Blog.Repository.UOW.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.LoginValidator
{
 
    public class UserValidator : AbstractValidator<User>
    {
        
        public UserValidator( )  
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad alanı boş geçilemez");
            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş geçilemez");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Hatalı giriş yaptınız");
            RuleFor(x => x.Email).MaximumLength(50).WithMessage("En fazla 50 karakter girebilirsiniz");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez");
            RuleFor(x => x.Password).Length(8, 12).WithMessage("Parolanız en az 8,en fazla 12 karakterden oluşmalıdır.");
            RuleFor(x => x.PasswordConfirm).NotEmpty().WithMessage("Bu alan boş geçilemez");
            RuleFor(x => x.PasswordConfirm).Equal(x => x.Password).WithMessage("Parolalar uyuşmuyor");
        }
    }
}
