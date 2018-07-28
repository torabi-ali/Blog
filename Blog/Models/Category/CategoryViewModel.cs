using Blog.DomainClass;
using System.ComponentModel.DataAnnotations;

namespace Blog.Models
{
    public class CategoryViewModel : ContentViewModel
    {
        public CategoryViewModel()
        {
            IsEnable = false;
        }

        [Required, MaxLength(32)]
        [Display(Name = "نام")]
        public string Name { get; set; }

        [MaxLength(64)]
        [Display(Name = "نام جایگزین")]
        public string AlternativeName { get; set; }

        [Display(Name = "دسته‌بندی پدر")]
        public CategoryViewModel ParentCategory { get; set; }

        public int? ParentCategoryId { get; set; }


        public static implicit operator CategoryViewModel(Category category)
        {
            return new CategoryViewModel
            {
                Id = category.Id,
                Name = category.Name,
                AlternativeName = category.AlternativeName,
                ParentCategory = category.ParentCategory,
                ParentCategoryId = category.ParentCategory?.Id,
                Title = category.Title,
                Url = category.Url,
                ImagePath = category.ImagePath,
                Summary = category.Summary,
                Text = category.Text,
                MetaDescription = category.MetaDescription,
                FocusKeyword = category.FocusKeyword,
                VisitCount = category.VisitCount,
                IsEnable = category.IsEnable,
                InsertUser = category.InsertUser,
                InsertDateTime = category.InsertDateTime,
                UpdateUser = category.UpdateUser,
                UpdateDateTime = category.UpdateDateTime,
                DeleteUser = category.DeleteUser,
                DeleteDateTime = category.DeleteDateTime
            };
        }

        public static implicit operator Category(CategoryViewModel categoryViewModel)
        {
            return new Category
            {
                Id = categoryViewModel.Id,
                Name = categoryViewModel.Name,
                AlternativeName = categoryViewModel.AlternativeName,
                ParentCategory = categoryViewModel.ParentCategory,
                ParentCategoryId = categoryViewModel.ParentCategoryId,
                Title = categoryViewModel.Title,
                Url = categoryViewModel.Url,
                ImagePath = categoryViewModel.ImagePath,
                Summary = categoryViewModel.Summary,
                Text = categoryViewModel.Text,
                MetaDescription = categoryViewModel.MetaDescription,
                FocusKeyword = categoryViewModel.FocusKeyword,
                VisitCount = categoryViewModel.VisitCount,
                IsEnable = categoryViewModel.IsEnable,
                InsertUser = categoryViewModel.InsertUser,
                InsertDateTime = categoryViewModel.InsertDateTime,
                UpdateUser = categoryViewModel.UpdateUser,
                UpdateDateTime = categoryViewModel.UpdateDateTime,
                DeleteUser = categoryViewModel.DeleteUser,
                DeleteDateTime = categoryViewModel.DeleteDateTime
            };
        }
    }
}