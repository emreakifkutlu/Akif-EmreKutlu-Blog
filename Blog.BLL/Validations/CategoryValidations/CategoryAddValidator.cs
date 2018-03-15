using Blog.DAL;
using Blog.DAL.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.CategoryValidations
{
    public class CategoryAddValidator : AbstractValidator<Category>
    {
        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Kategori alanı boş geçilemez");
        }
    }
}
