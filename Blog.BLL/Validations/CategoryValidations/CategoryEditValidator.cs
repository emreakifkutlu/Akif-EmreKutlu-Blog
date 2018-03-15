using Blog.DAL.Entities;
using Blog.Repository.Abstract;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.BLL.Validations.CategoryValidations
{
   public class CategoryEditValidator : AbstractValidator<Category>
    {
        private readonly ICategoryRepository _catRepo;
        public CategoryEditValidator(ICategoryRepository catRepo)
        {
            _catRepo = catRepo;
            RuleFor(x => x.Name).NotEmpty().WithMessage("Bu alan boş geçilmez");
            RuleFor(x => x).Must(HasCategoryName).WithMessage("Daha önce böyle bir kategori girişi yapılmıştır.");
            RuleFor(x => x.Description).MaximumLength(200).WithMessage("200 karakter sınırı vardır");
        }
        public bool HasCategoryName(Category model)
        {
           
            var data = _catRepo.Where(x => x.Name == model.Name && x.Id != model.Id).FirstOrDefault();
            if (data == null)
            {
                return true;
            }
            return false;
        }
    }
}
