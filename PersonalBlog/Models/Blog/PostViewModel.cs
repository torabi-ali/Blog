using Microsoft.AspNetCore.Mvc;
using PersonalBlog.Extensions;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PersonalBlog.Models
{
    public class PostViewModel : ContentViewModel
    {
        public PostViewModel()
        {
            IsEnable = false;
            IsPin = false;
        }

        [Display(Name = "نام منبع")]
        public string SourceName { get; set; }

        [Display(Name = "آدرس منبع")]
        public string SourceUrl { get; set; }

        [Display(Name = "سنجاق کردن")]
        public bool IsPin { get; set; }

        [HiddenInput]
        [Display(Name = "دسته‌بندی")]
        public string CategoryList { get; set; }


        public static implicit operator PostViewModel(Post post)
        {
            return new PostViewModel
            {
                Id = post.Id,
                SourceName = post.SourceName,
                SourceUrl = post.SourceUrl,
                IsPin = post.IsPin,
                CategoryList = post.Categories.ToPrepare(),
                Title = post.Title,
                Url = post.Url,
                ImagePath = post.ImagePath,
                Summary = post.Summary,
                Text = post.Text,
                MetaDescription = post.MetaDescription,
                FocusKeyword = post.FocusKeyword,
                VisitCount = post.VisitCount,
                IsEnable = post.IsEnable,
                InsertUser = post.InsertUser,
                InsertDateTime = post.InsertDateTime,
                UpdateUser = post.UpdateUser,
                UpdateDateTime = post.UpdateDateTime,
                DeleteUser = post.DeleteUser,
                DeleteDateTime = post.DeleteDateTime
            };
        }

        public static implicit operator Post(PostViewModel postViewModel)
        {
            return new Post
            {
                Id = postViewModel.Id,
                SourceName = postViewModel.SourceName,
                SourceUrl = postViewModel.SourceUrl,
                IsPin = postViewModel.IsPin,
                Title = postViewModel.Title,
                Url = postViewModel.Url,
                ImagePath = postViewModel.ImagePath,
                Summary = postViewModel.Summary,
                Text = postViewModel.Text,
                MetaDescription = postViewModel.MetaDescription,
                FocusKeyword = postViewModel.FocusKeyword,
                VisitCount = postViewModel.VisitCount,
                IsEnable = postViewModel.IsEnable,
                InsertUser = postViewModel.InsertUser,
                InsertDateTime = postViewModel.InsertDateTime,
                UpdateUser = postViewModel.UpdateUser,
                UpdateDateTime = postViewModel.UpdateDateTime,
                DeleteUser = postViewModel.DeleteUser,
                DeleteDateTime = postViewModel.DeleteDateTime
            };
        }
    }
}
